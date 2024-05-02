
using OpenCvSharp;
using OpenCvSharp.WpfExtensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Vision_Inspection.Core;
using Vision_Inspection.VisionLogic.Funtions;

namespace Vision_Inspection.Camera
{
    public class Camera : ObseriableObject
    {
        private static readonly Camera instance = new Camera();
        public static Camera GetCamera() => instance;

        private CancellationTokenSource? _cancellationTokenSource;
        /// <summary>
        /// Camera last frame readded
        /// </summary>
        private Mat _LastMatFrame = new();
        public Mat LastMatFrame
        {
            get { return _LastMatFrame; }
            set
            {
                if (_LastMatFrame != value && value != null)
                {
                    _LastMatFrame = value;
                    NotifyPropertyChanged(nameof(LastBitmapSource));
                }
            }
        }
        private BitmapSource _LastBitmapSource;
        public BitmapSource LastBitmapSource
        {
            get
            {
                if (LastMatFrame != null && !LastMatFrame.Empty())
                {
                    var bi = LastMatFrame.ToBitmapSource();
                    bi.Freeze();
                    _LastBitmapSource = bi;
                    return _LastBitmapSource;
                }
                else
                {
                    return _LastBitmapSource;
                }
            }
        }

        private Mat _CropedMat = new Mat();
        public Mat CropedMat
        {
            get { return _CropedMat; }
            set
            {
                if (_CropedMat != value)
                {
                    _CropedMat = value;
                    NotifyPropertyChanged(nameof(CropedMat));
                    NotifyPropertyChanged(nameof(CroppedBitmapSource));
                }
            }
        }
        private BitmapSource _CroppedBitmapSource;
        public BitmapSource CroppedBitmapSource
        {
            get
            {
                if (CropedMat != null && !CropedMat.Empty())
                {
                    var bi = CropedMat.ToBitmapSource();
                    bi.Freeze();
                    _CroppedBitmapSource = bi;
                    return _CroppedBitmapSource;
                }
                else
                {
                    return _CroppedBitmapSource;
                }
            }
        }


        private bool _StopRequest = false;

        BackGroundSubtraction subtraction = new BackGroundSubtraction();

        private Camera()
        {
            Thread capture = new Thread(Start);
            capture.Start();
        }

        public async void Start()
        {
            VideoCapture capture = new VideoCapture(0, VideoCaptureAPIs.DSHOW);
            capture.FrameWidth = (int)GlobalSize.Default.Width;
            capture.FrameHeight = (int)GlobalSize.Default.Height;
            capture.Fps = 50;
            capture.FourCC = "MJPG";
            capture.Brightness= 128;
            capture.Contrast= 128;
            capture.Saturation = 128;
            capture.Exposure = -4;
            int frameCount = 0;


            while (true)
            {
                using (Mat frame = capture.RetrieveMat())
                {
                    if (frame != null && !frame.Empty())
                    {
                       
                        if (subtraction.BackGround.Empty())
                        {
                            subtraction.BackGround = frame.Clone();
                        }
                        else
                        {
                            frameCount++;
                            subtraction.CreateForegroundMask(frame);
                            CropedMat = subtraction.Foreground.Clone();
                            LastMatFrame = subtraction.ForegroundMask.Clone();

                        }
                    }
                    capture.Grab();
                    await Task.Delay(30);
                        if (_StopRequest) return;
                }
            }
        }
        public void Stop()
        {
            this._StopRequest = true;
        }

        public void GetBackground()
        {
            this.subtraction.BackGround = new Mat();
        }
        public void CreateForegroundMark()
        {
            this.subtraction.haveObject = false;
        }
    }

}

