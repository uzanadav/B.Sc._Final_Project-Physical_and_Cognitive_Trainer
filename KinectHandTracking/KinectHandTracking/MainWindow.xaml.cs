using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using swf = System.Windows.Forms;
using System.IO.Ports;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace KinectHandTracking
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Members
        private SerialPort myport;
        KinectSensor _sensor;
        MultiSourceFrameReader _reader;
        IList<Body> _bodies;

        #endregion

        #region Constructor

        public MainWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Event handlers

        private void Window_Loaded(object sender, RoutedEventArgs e)// loading the camera window
        {
            InitializeComponent();
            init();
            _sensor = KinectSensor.GetDefault();

            if (_sensor != null)
            {
                _sensor.Open();

                _reader = _sensor.OpenMultiSourceFrameReader(FrameSourceTypes.Color | FrameSourceTypes.Depth | FrameSourceTypes.Infrared | FrameSourceTypes.Body);
                _reader.MultiSourceFrameArrived += Reader_MultiSourceFrameArrived;
            }
        }

        private void Window_Closed(object sender, EventArgs e)//what happens if we close the window
        {
            if (_reader != null)
            {
                _reader.Dispose();
            }

            if (_sensor != null)
            {
                _sensor.Close();
            }
        }

        void Reader_MultiSourceFrameArrived(object sender, MultiSourceFrameArrivedEventArgs e)
        {
            var reference = e.FrameReference.AcquireFrame();

            // Color
            using (var frame = reference.ColorFrameReference.AcquireFrame())
            {
                if (frame != null)
                {
                    camera.Source = frame.ToBitmap();
                }
            }

            // Body
            using (var frame = reference.BodyFrameReference.AcquireFrame())//getting the player moves by the camera
            {
                if (frame != null)
                {
                    canvas.Children.Clear();

                    _bodies = new Body[frame.BodyFrameSource.BodyCount];

                    frame.GetAndRefreshBodyData(_bodies);

                    foreach (var body in _bodies)
                    {
                        if (body != null)
                        {
                            if (body.IsTracked)
                            {
                                // Find the joints
                                Joint handRight = body.Joints[JointType.HandRight];
                                Joint thumbRight = body.Joints[JointType.ThumbRight];

                                Joint handLeft = body.Joints[JointType.HandLeft];
                                Joint thumbLeft = body.Joints[JointType.ThumbLeft];

                                // Draw hands and thumbs
                                canvas.DrawHand(handRight, _sensor.CoordinateMapper);
                                canvas.DrawHand(handLeft, _sensor.CoordinateMapper);
                                canvas.DrawThumb(thumbRight, _sensor.CoordinateMapper);
                                canvas.DrawThumb(thumbLeft, _sensor.CoordinateMapper);

                                // Find the hand states
                                string rightHandState = "-";
                                string leftHandState = "-";
                                

                                if (body.Joints[JointType.WristLeft].Position.Y >= body.Joints[JointType.ShoulderLeft].Position.Y
                                    && body.Joints[JointType.ElbowLeft].Position.Y >= body.Joints[JointType.SpineMid].Position.Y
                                    && body.Joints[JointType.ElbowLeft].Position.X >= body.Joints[JointType.WristLeft].Position.X)// setting the hand getuers of the game 

                                {
                                    leftHandState = "LEFT Stright-  #1";
                                    //System.Windows.Forms.LayoutEventArgs layout;
                                    
                                   // this.SuspendLayout();
                                    myport.WriteLine("R");//yellow lamp
                                    System.Threading.Thread.Sleep(1000);
                                    //this.ResumeLayout();
                                    leftHandState = "LEFT Stright - #1";
                                }

                                if (body.Joints[JointType.WristLeft].Position.Y >= body.Joints[JointType.SpineMid].Position.Y
                                    && body.Joints[JointType.WristLeft].Position.Y <= body.Joints[JointType.ShoulderLeft].Position.Y
                                    && body.Joints[JointType.ElbowLeft].Position.Y >= body.Joints[JointType.SpineMid].Position.Y
                                    && body.Joints[JointType.ElbowLeft].Position.X <= body.Joints[JointType.WristLeft].Position.X)// setting the hand getuers of the game
                                {
                                    leftHandState = "lEFT Fold - #2";
                                     myport.WriteLine("B");//green lamp
                                    System.Threading.Thread.Sleep(1000);
                                    leftHandState = "lEFT Fold - #2";

                                }


                                if (body.Joints[JointType.WristRight].Position.Y >= body.Joints[JointType.ShoulderRight].Position.Y
                                    && body.Joints[JointType.ElbowRight].Position.Y >= body.Joints[JointType.SpineMid].Position.Y
                                    && body.Joints[JointType.ElbowRight].Position.X <= body.Joints[JointType.WristRight].Position.X)// setting the hand getuers of the game
                                {
                                    rightHandState = "RIGHT Stright -# 3";
                                    myport.WriteLine("G");//Green light
                                    System.Threading.Thread.Sleep(1000);
                                    rightHandState = "RIGHT Stright - # 3";




                                }
                                if (body.Joints[JointType.WristRight].Position.Y >= body.Joints[JointType.SpineMid].Position.Y
                                    && body.Joints[JointType.WristRight].Position.Y <= body.Joints[JointType.ShoulderRight].Position.Y
                                    && body.Joints[JointType.ElbowRight].Position.Y >= body.Joints[JointType.SpineMid].Position.Y
                                    && body.Joints[JointType.ElbowRight].Position.X >= body.Joints[JointType.WristRight].Position.X)// setting the hand getuers of the game
                                {
                                    rightHandState = "RIGHT Fold - #4";
                                    
                                    myport.WriteLine("Y");//
                                    System.Threading.Thread.Sleep(1000);
                                    rightHandState = "RIGHT Fold - #4";
                                }


                                tblRightHandState.Text = rightHandState;
                                tblLeftHandState.Text = leftHandState;
                            }
                        }
                    }
                }
            }
        }


        #endregion


        private void displaymessage()
        {
            System.Threading.Thread.Sleep(1000);
            // MessageBox.Show("message box number 1");
        }

        public void SuspendLayout() { }
        public void ResumeLayout() { }

        private void On_btn_Click(object sender, RoutedEventArgs e)
        {
            InitializeComponent();
            init();
        }
        private void init()//conecting to the arduino
        {
            try//coneection details
            {
                myport = new SerialPort();
                myport.BaudRate = 9600;
                myport.PortName = "COM5";
                myport.Open();
            }
            catch (Exception)
            {
                swf.MessageBox.Show("Error!!");
            }

        }

        private void on_Click(object sender, RoutedEventArgs e)//sending the output of the camera system to the arduino
        {
            myport.WriteLine("R");

        }

        private void off_Click(object sender, RoutedEventArgs e)
        {
            myport.WriteLine("Y");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            myport.WriteLine("B");

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            myport.WriteLine("G");

        }
    }

}