using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Bowling
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        string file;

        private void Upload_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.DefaultExt = ".txt";
            fileDialog.Filter = "Text documents (.txt)|*.txt";
            if (fileDialog.ShowDialog() == true)
            {
                file = fileDialog.FileName;
            
            var lines = File.ReadAllLines(file);
            var count = lines.Length / 2;
            int maxplayers = 6;
                using (TextReader reader = File.OpenText(file))
                {
                    string[] Players = new string[maxplayers];
                    string[] Scores = new string[maxplayers];
                    int[][] ScoresInt = new int[count][];
                    string[] integerStrings = new string[count];
                    int[] final_score = new int[6];
                    for (int i = 0; i < count; i++)
                    {
                        Players[i] = reader.ReadLine();
                        Scores[i] = reader.ReadLine();
                        integerStrings = Scores[i].Split(new char[] { ',', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                        ScoresInt[i] = Array.ConvertAll(integerStrings, int.Parse);
                        final_score[i] = ScoresInt[i].Sum();
                    }
                    for (int i = 0; i < count; i++)
                    {
                        for (int j = 0; j < 22; j += 2)
                        {
                            //spare
                            if (ScoresInt[i][j] + ScoresInt[i][j + 1] == 10)
                            {
                                final_score[i] += ScoresInt[i][j + 2];
                            }
                            //strike
                            if (ScoresInt[i][j] == 10)
                            {
                                final_score[i] += ScoresInt[i][j + 2] + ScoresInt[i][j + 3];
                            }
                        }

                    }
                    if (count < maxplayers)
                    {
                        for (int i = count; i < 6; i++)
                        {
                            Players[i] = "brak";
                            Scores[i] = "brak";
                            final_score[i] = 0;
                        }
                    }

                    Player1Label.Content = Players[0];
                    Player2Label.Content = Players[1];
                    Player3Label.Content = Players[2];
                    Player4Label.Content = Players[3];
                    Player5Label.Content = Players[4];
                    Player6Label.Content = Players[5];
                    Score1Label.Content = Scores[0];
                    Score2Label.Content = Scores[1];
                    Score3Label.Content = Scores[2];
                    Score4Label.Content = Scores[3];
                    Score5Label.Content = Scores[4];
                    Score6Label.Content = Scores[5];
                    FinalScore1.Content = final_score[0];
                    FinalScore2.Content = final_score[1];
                    FinalScore3.Content = final_score[2];
                    FinalScore4.Content = final_score[3];
                    FinalScore5.Content = final_score[4];
                    FinalScore6.Content = final_score[5];

                }
            }
        }
    }
}
