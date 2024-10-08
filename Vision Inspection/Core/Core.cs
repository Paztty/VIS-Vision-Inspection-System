﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace Vision_Inspection.Core
{
    public class ObseriableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public class RelayCommand<T> : ICommand
    {
        #region Fields

        readonly Action<T> _execute = null;
        readonly Predicate<T> _canExecute = null;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="DelegateCommand{T}"/>.
        /// </summary>
        /// <param name="execute">Delegate to execute when Execute is called on the command.  This can be null to just hook up a CanExecute delegate.</param>
        /// <remarks><seealso cref="CanExecute"/> will always return true.</remarks>
        public RelayCommand(Action<T> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Creates a new command.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public RelayCommand(Action<T> execute, Predicate<T> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            _execute = execute;
            _canExecute = canExecute;
        }

        #endregion

        #region ICommand Members

        ///<summary>
        ///Defines the method that determines whether the command can execute in its current state.
        ///</summary>
        ///<param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        ///<returns>
        ///true if this command can be executed; otherwise, false.
        ///</returns>
        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute((T)parameter);
        }

        ///<summary>
        ///Occurs when changes occur that affect whether or not the command should execute.
        ///</summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        ///<summary>
        ///Defines the method to be called when the command is invoked.
        ///</summary>
        ///<param name="parameter">Data used by the command. If the command does not require data to be passed, this object can be set to <see langword="null" />.</param>
        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }

        #endregion
    }
    public static class GlobalSize
    {
        public static readonly Size Default = new Size(1920, 1080);
        public static (double X, double Y) GetGlobalLocation((double X, double Y) displayPoint, (double Width, double Height) displayParentSize)
        {
            (double X, double Y) globalPoint = new();
            globalPoint.X = Default.Width / displayParentSize.Width * displayPoint.X;
            globalPoint.Y = Default.Height / displayParentSize.Height * displayPoint.Y;
            return globalPoint;
        }
        public static (double Width, double Height) GetGlobalSize((double Width, double Height) displaySize, (double Width, double Height) displayParentSize)
        {
            (double Width, double Height) globalSize = new();
            globalSize.Width =Default.Width / displayParentSize.Width * displaySize.Width;
            globalSize.Height = Default.Height/ displayParentSize.Height * displaySize.Height;
            return globalSize;
        }
        public static (double X, double Y) GetDisplayLocation((double X, double Y) globalPoint, (double Width, double Height) displayParentSize)
        {
            (double X, double Y) displayPoint = new();
            displayPoint.X = displayParentSize.Width / Default.Width * globalPoint.X;
            displayPoint.Y = displayParentSize.Height / Default.Height * globalPoint.Y;
            return displayPoint;
        }
        public static (double Width, double Height) GetDisplaySize((double Width, double Height) globalSize, (double Width, double Height) displayParentSize)
        {
            (double Width, double Height) displaySize = new();
            displaySize.Width = displayParentSize.Width / Default.Width * globalSize.Width;
            displaySize.Height = displayParentSize.Height / Default.Height * globalSize.Height;
            return displaySize;
        }
    }
}
