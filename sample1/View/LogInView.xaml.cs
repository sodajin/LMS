using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace sample1.View
{
    
    public partial class LogInView : UserControl
    {
        private readonly DispatcherTimer timer;
        
        public LogInView()
        {
            InitializeComponent();

            timer = new DispatcherTimer();
            timer.Tick += TimerTick;
            timer.Interval = TimeSpan.FromSeconds(5);
        }

        // Grant or deny access upon clicking login button
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = userName.Text;
            string password = enterPassword.Password;

            string validUsername = "demo";
            string validPassword = "password";

            if (username == validUsername && password == validPassword)
            {
                txtSuccessMessage.Visibility = Visibility.Visible;
                txtSuccessMessage.Text = "WELCOME";
            }
            else
            {
                txtErrorMessage.Visibility = Visibility.Visible;
                
                //StartShakeAnimation(txtErrorMessage);
                
                txtErrorMessage.Text = "INVALID CREDENTIALS";
                
                //timer.Start();
            }
        }

        // Shake error message
        //private void StartShakeAnimation(TextBlock textBlock)
        //{
        //    TimeSpan duration = TimeSpan.FromSeconds(1);

        //    DoubleAnimationUsingKeyFrames animation = new DoubleAnimationUsingKeyFrames {Duration = duration};
        
        //    // Define keyframes for the animation
        //    double[] keyframeValues = { 0, -5, 5, -5, 5, -5, 5, -5, 5, -5, 0 };
        //    double[] keyframeTimes = { 0, 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1 };

        //    for (int i = 0; i < keyframeValues.Length; i++)
        //    {
        //        animation.KeyFrames.Add(new DiscreteDoubleKeyFrame(keyframeValues[i], KeyTime.FromTimeSpan(TimeSpan.FromSeconds(keyframeTimes[i]))));
        //    }

        //    textBlock.RenderTransform = new TranslateTransform();
        //    textBlock.RenderTransform.BeginAnimation(TranslateTransform.XProperty, animation);
        //}

        // Make error message disappear
        private void TimerTick(object sender, EventArgs e)
        {
            timer.Stop();

            txtErrorMessage.Visibility = Visibility.Collapsed;
            txtErrorMessage.Text = string.Empty;
        }

    }

}
