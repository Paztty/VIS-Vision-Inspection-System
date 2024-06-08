using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Converters;
using System.Windows.Media.Media3D;
using Vision_Inspection.Core;

namespace Vision_Inspection.VisionLogic.Funtions
{
    public class BackGroundSubtraction : ObseriableObject
    {
        public Mat BackGround = new();
        public Mat ForegroundMask = new();
        public Mat Foreground = new();
        public OpenCvSharp.BackgroundSubtractorMOG kNN = OpenCvSharp.BackgroundSubtractorMOG.Create();
        public bool haveObject = false;
        public void CreateForegroundMask(Mat mat)
        {
            if (BackGround.Empty() || mat.Empty()) return;
            var diff = new Mat();
            try
            {
                var GrayMat = mat.CvtColor(ColorConversionCodes.BGR2GRAY);
                //ForegroundMask = diff;
                var GrayBackground = BackGround.CvtColor(ColorConversionCodes.BGR2GRAY);
                Cv2.Absdiff(GrayMat, GrayBackground, diff);
                //ForegroundMask = diff;
            }
            catch (Exception)
            {
                return;
            }
            diff.Threshold(30, 255, ThresholdTypes.Tozero);
            //ForegroundMask = diff;
            diff.GaussianBlur(new Size(5, 5), 2);
            //ForegroundMask = diff;
            diff = diff.Threshold(128, 255, ThresholdTypes.Otsu);
            ForegroundMask = diff;
            OpenCvSharp.Point[][] contour;
            HierarchyIndex[] hierarchy;
            Cv2.FindContours(diff, out contour, out hierarchy, RetrievalModes.External, ContourApproximationModes.ApproxSimple);
            var maxIndex = 0;
            double maxArea = 0;
            if (contour.Length > 0)
            {
                for (int i = 0; i < contour.Count(); i++)
                {
                    var area = Cv2.ContourArea(contour[i]);
                    if (area > maxArea)
                    {
                        maxArea = area;
                        maxIndex = i;
                    }
                }

                var rRect = Cv2.MinAreaRect(contour[maxIndex]);
                Point2f[] vertices2f = rRect.Points();
                Point[] vertices = new Point[4];
                bool Outside = false;
                for (int i = 0; i < 4; i++)
                {
                    vertices[i] = (Point)vertices2f[i];
                    if (vertices[i].X <= 1 || vertices[i].X >= GlobalSize.Default.Width) Outside = true;
                    if (vertices[i].Y <= 1 || vertices[i].Y >= GlobalSize.Default.Height) Outside = true;
                }
                var color = Outside ? new Scalar(0, 0, 255) : new Scalar(0, 255, 0);

                mat.Line(vertices[0], vertices[1], new Scalar(0, 0, 255));
                mat.Line(vertices[1], vertices[2], new Scalar(0, 0, 255));
                mat.Line(vertices[2], vertices[3], new Scalar(0, 0, 255));
                mat.Line(vertices[3], vertices[0], new Scalar(0, 0, 255));
                //ForegroundMask = mat;
                if (Outside == false && maxArea > 5000 && !haveObject)
                //if (Outside == false && maxArea > 5000)
                    {
                    haveObject = true;
                    Debug.WriteLine(maxArea);
                    Cv2.Rectangle(mat, Cv2.BoundingRect(vertices), color, 2);
                    Mat crop = CropMinRectangle(mat, rRect);
                    Foreground = crop;
                    }
                else if (maxArea < 5000 || Outside == true)
                {
                    haveObject = false;
                }
            }
            else
            {
                haveObject = false;
                BackGround = mat.Clone();
            }
            //ForegroundMask = mat;
        }

        public static Mat CropMinRectangle(Mat src, RotatedRect rect)
        {
            Mat rImage = new Mat();

            var width = (int)rect.Size.Width;
            var height = (int)rect.Size.Height;

            var box = Cv2.BoxPoints(rect);

            Point2f[] box_dst = new Point2f[]
            {
                new Point2f(0, height - 1),
                new Point2f(0, 0),
                new Point2f(width - 1, 0),
                new Point2f(width - 1, height - 1),
            };

            var M = Cv2.GetPerspectiveTransform(box, box_dst);

            Cv2.WarpPerspective(src, rImage, M, new Size(width, height));

            if (rImage.Width < rImage.Height)
            {
                Cv2.Rotate(rImage, rImage, RotateFlags.Rotate90Clockwise);
            }
            Cv2.Resize(rImage, rImage, new Size(1920, 1080));

            return rImage;
        }
        public static void DrawCenter<Mat>(Mat mat, Point2f center)
        {
            
        }    
    }
}
