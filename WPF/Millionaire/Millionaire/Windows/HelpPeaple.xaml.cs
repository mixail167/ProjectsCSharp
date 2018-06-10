using LiveCharts;
using LiveCharts.Wpf;
using Millionaire.Classes;
using System;
using System.Windows;

namespace Millionaire.Windows
{
    /// <summary>
    /// Логика взаимодействия для HelpPeaple.xaml
    /// </summary>
    public partial class HelpPeaple : Window
    {
        Data currentQuestion;
        int[] voices;
        int indexTrueAnswer;

        public HelpPeaple(Data currentQuestion)
        {
            InitializeComponent();
            WAVPlayer.PlaySound(Properties.Resources.кто_хочет_стать_миллионером_помощь_зала);
            this.currentQuestion = currentQuestion;
            voices = new int[4];
            ColumnSeries.LabelPoint = point => point.Y + "%";
        }

        private void HelpPeapleWindow_ContentRendered(object sender, EventArgs e)
        {
            if (currentQuestion.Answers[0].EndsWith(currentQuestion.TrueAnswer))
            {
                indexTrueAnswer = 0;
            }
            else if (currentQuestion.Answers[1].EndsWith(currentQuestion.TrueAnswer))
            {
                indexTrueAnswer = 1;
            }
            else if (currentQuestion.Answers[2].EndsWith(currentQuestion.TrueAnswer))
            {
                indexTrueAnswer = 2;
            }
            else
            {
                indexTrueAnswer = 3;
            }
            Random r = new Random();
            double koef = r.Next(70, 91)* 1.0 / 100.0;
            for (int i = 0; i < 50; i++)
            {
                if (r.NextDouble() <= koef)
                {
                    voices[indexTrueAnswer]+=2;
                }
                else
                {
                    while (true)
                    {
                        int index = r.Next(0, 4);
                        if (!index.Equals(indexTrueAnswer) && !currentQuestion.Answers[index].Equals(string.Empty))
                        {
                            voices[index]+=2;
                            break;
                        }
                    }
                }
            }
            ColumnSeries.Values = new ChartValues<int> { voices[0], voices[1], voices[2], voices[3] };
        }        

        private void HelpPeapleWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            WAVPlayer.StopSound();
        }
    }
}
