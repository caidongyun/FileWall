using System;


namespace VitaliiPianykh.FileWall.Client
{
    public class LogViewPresenter
    {
        private ILogView _LogView;

        /// <summary>Creates new LogViewPresenter.</summary>
        public LogViewPresenter(LogViewModel logViewModel)
        {
            // Check parameters.
            if(logViewModel == null)
                throw new ArgumentNullException("logViewModel");
            LogViewModel = logViewModel;
        }

        public LogViewModel LogViewModel { get; private set; }

        public ILogView LogView
        {
            get { return _LogView; }
            set
            {
                _LogView = value;
                if (_LogView != null)
                {
                    _LogView.Data = LogViewModel.Data;
                    _LogView.DetailsVisible = false;
                }
            }
        }

        public bool DetailsVisible
        {
            get
            {
                if (_LogView == null)
                    throw new InvalidOperationException("LogView is null");
                return _LogView.DetailsVisible;
            }
            set
            {
                if (_LogView == null)
                    throw new InvalidOperationException("LogView is null");
                _LogView.DetailsVisible = value;
            }
        }
    }
}
