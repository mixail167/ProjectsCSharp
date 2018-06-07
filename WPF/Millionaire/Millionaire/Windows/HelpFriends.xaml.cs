using Millionaire.Classes;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace Millionaire
{
    /// <summary>
    /// Логика взаимодействия для HelpFriends.xaml
    /// </summary>
    public partial class HelpFriends : Window
    {
        DispatcherTimer timer;
        int time;
        int index;
        double koef;
        Data currentQuestion;
        public bool next;

        public HelpFriends(Data currentQuestion)
        {
            InitializeComponent();
            Random r = new Random();
            time = r.Next(5, 15);
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0, 0, 1);
            index = 0;
            this.currentQuestion = currentQuestion;
            next = false;
        }

        void Processing(Image image, TextBlock textBlock, TextBlock description, double koef)
        {
            ImageFriend.Source = image.Source;
            TextBlockFriend.Text = textBlock.Text;
            TextBlockDescription.Text = description.Text;
            timer.IsEnabled = true;
            WAVPlayer.PlaySound(Properties.Resources.gudok);
            GridCall.Visibility = Visibility.Visible;
            this.koef = koef;
        }

        private void timerTick(object sender, EventArgs e)
        {
            index++;
            if (index % 4 == 0)
            {
                TextBlockCall.Text = TextBlockCall.Text.Remove(TextBlockCall.Text.Length - 3);
            }
            else
            {
                TextBlockCall.Text += ".";
            }
            if (time.Equals(index))
            {
                Random r = new Random();
                if (r.NextDouble() >= koef)
                {
                    if (currentQuestion.Answers[0].EndsWith(currentQuestion.TrueAnswer))
                    {
                        SetTextBlock('A');
                    }
                    else if (currentQuestion.Answers[1].EndsWith(currentQuestion.TrueAnswer))
                    {
                        SetTextBlock('B');
                    }
                    else if (currentQuestion.Answers[2].EndsWith(currentQuestion.TrueAnswer))
                    {
                        SetTextBlock('C');
                    }
                    else
                    {
                        SetTextBlock('D');
                    }
                }
                else
                {
                    while (true)
                    {
                        int i = r.Next(0, 4);
                        if (!currentQuestion.Answers[i].EndsWith(currentQuestion.TrueAnswer) &&
                            !currentQuestion.Answers[i].Equals(string.Empty))
                        {
                            SetTextBlock(currentQuestion.Answers[i][0]);
                            break;
                        }
                    }
                }
            }
        }

        void SetTextBlock(char answer)
        {
            WAVPlayer.StopSound();
            timer.IsEnabled = false;
            TextBlockCall.Text = string.Format("{0} считает, что правильный ответ - \"{1}\".", TextBlockFriend.Text, answer);
            next = true;
        }

        private void ImageBred_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Processing(ImageBred, TextBlockBred, DescriptionBred, 0.2);
        }

        private void ImageDjoli_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Processing(ImageDjoli, TextBlockDjoli, DescriptionDjoli, 0.35);
        }

        private void ImageJeLo_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Processing(ImageJeLo, TextBlockJeLo, DescriptionJeLo, 0.5);
        }

        private void TextBlockBred_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Processing(ImageBred, TextBlockBred, DescriptionBred, 0.2);
        }

        private void TextBlockDjoli_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Processing(ImageDjoli, TextBlockDjoli, DescriptionDjoli, 0.35);
        }

        private void TextBlockJeLo_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Processing(ImageJeLo, TextBlockJeLo, DescriptionJeLo, 0.5);
        }

        private void HelpFriendsWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            WAVPlayer.StopSound();
        }

        private void SetColor(Border border, SolidColorBrush solidColorBrush)
        {
            border.Background = solidColorBrush;
        }

        private void SetColor(TextBlock textBlock, SolidColorBrush solidColorBrush)
        {
            textBlock.Background = solidColorBrush;
        }

        private void ImageBred_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            SetColor(BorderBred, Brushes.Gray);
        }

        private void ImageBred_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            SetColor(BorderBred, null);
        }

        private void ImageDjoli_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            SetColor(BorderDjoli, Brushes.Gray);
        }

        private void ImageDjoli_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            SetColor(BorderDjoli, null);
        }

        private void ImageJeLo_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            SetColor(BorderJeLo, Brushes.Gray);
        }

        private void ImageJeLo_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            SetColor(BorderJeLo, null);
        }

        private void TextBlockBred_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            SetColor(TextBlockBred, Brushes.Gray);
        }

        private void TextBlockBred_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            SetColor(TextBlockBred, null);
        }

        private void TextBlockDjoli_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            SetColor(TextBlockDjoli, Brushes.Gray);
        }

        private void TextBlockDjoli_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            SetColor(TextBlockDjoli, null);
        }

        private void TextBlockJeLo_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            SetColor(TextBlockJeLo, Brushes.Gray);
        }

        private void TextBlockJeLo_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            SetColor(TextBlockJeLo, null);
        }
    }
}
