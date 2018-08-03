using SharpGL;
using SharpGL.SceneGraph.Assets;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SnakeNew
{
    enum Direction { Up, Down, Left, Right };

    public partial class Form1 : Form
    {
        bool pause;
        bool endOfGame;
        uint score;
        const int scale = 20;
        Direction direction;
        List<Coordinate> snake;
        List<Coordinate> bombs;
        Coordinate apple;
        Random random;
        Complexity complexity;
        List<Complexity> complexities;
        OpenGL openGL;
        bool run;

        public Form1()
        {
            InitializeComponent();
            complexities = new List<Complexity>();
            complexities.Add(new Complexity("record0", "Очень просто", 1, 1000));
            complexities.Add(new Complexity("record1", "Просто", 2, 120));
            complexities.Add(new Complexity("record2", "Средне", 3, 90));
            complexities.Add(new Complexity("record3", "Сложно", 4, 60));
            complexities.Add(new Complexity("record4", "Очень сложно", 5, 30));
            complexity = complexities[0];
            timer1.Interval = complexity.Interval;
            random = new Random();
        }

        void EnableComponents(bool flag)
        {
            новаяИграToolStripMenuItem.Enabled = flag;
            уровеньСложностиToolStripMenuItem.Enabled = flag;
            timer1.Enabled = !flag;
            endOfGame = flag;
            run = !flag;
            if (flag)
            {
                toolStripStatusLabel1.Text = string.Empty;
            }
            score = 0;
            pause = false;
        }

        void DrawImage(OpenGL openGL, Image image, Coordinate coord, int scale)
        {
            Texture texture = new Texture();
            texture.Create(openGL, (Bitmap)image);
            texture.Bind(openGL);

            openGL.Begin(OpenGL.GL_QUADS);
            openGL.TexCoord(0.0f, 1.0f); openGL.Vertex(coord.X, coord.Y);
            openGL.TexCoord(0.0f, 0.0f); openGL.Vertex(coord.X, coord.Y + scale);
            openGL.TexCoord(1.0f, 0.0f); openGL.Vertex(coord.X + scale, coord.Y + scale);
            openGL.TexCoord(1.0f, 1.0f); openGL.Vertex(coord.X + scale, coord.Y);
            openGL.End();
        }

        void PaintScene()
        {
            openGL.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            openGL.ClearColor(1f, 1f, 1f, 1f);
            if (run)
            {
                DrawImage(openGL, Properties.Resources.Apple, apple, scale);
                foreach (Coordinate item in bombs)
                {
                    DrawImage(openGL, Properties.Resources.bomb, item, scale);
                }
                Image image = Properties.Resources.StartSegment;
                switch (direction)
                {
                    case Direction.Up:
                        break;
                    case Direction.Down:
                        image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        break;
                    case Direction.Left:
                        image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        break;
                    case Direction.Right:
                        image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        break;
                }
                DrawImage(openGL, image, snake[0], scale);                
                for (int i = 1; i < snake.Count - 1; i++)
                {
                    if (snake[i - 1].X == snake[i + 1].X && (snake[i - 1].Y + 2 * scale == snake[i + 1].Y || snake[i - 1].Y - 2 * scale == snake[i + 1].Y))
                    {
                        image = Properties.Resources.DefaultSegment;
                    }
                    else if (snake[i - 1].Y == snake[i + 1].Y && (snake[i - 1].X + 2 * scale == snake[i + 1].X || snake[i - 1].X - 2 * scale == snake[i + 1].X))
                    {
                        image = Properties.Resources.DefaultSegment;
                        image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    }
                    else if ((snake[i - 1].X + scale == snake[i + 1].X &&
                              snake[i - 1].Y + scale == snake[i + 1].Y &&
                              snake[i].X == snake[i - 1].X &&
                              snake[i].Y == snake[i + 1].Y) ||
                             (snake[i - 1].X - scale == snake[i + 1].X &&
                              snake[i - 1].Y - scale == snake[i + 1].Y &&
                              snake[i].X == snake[i + 1].X &&
                              snake[i].Y == snake[i - 1].Y))
                    {
                        image = Properties.Resources.RotateSegment;
                        image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    }
                    else if ((snake[i - 1].X - scale == snake[i + 1].X &&
                              snake[i - 1].Y + scale == snake[i + 1].Y &&
                              snake[i].X == snake[i + 1].X &&
                              snake[i].Y == snake[i - 1].Y) ||
                             (snake[i - 1].X + scale == snake[i + 1].X &&
                              snake[i - 1].Y - scale == snake[i + 1].Y &&
                              snake[i].X == snake[i - 1].X &&
                              snake[i].Y == snake[i + 1].Y))
                    {
                        image = Properties.Resources.RotateSegment;
                    }
                    else if ((snake[i - 1].X + scale == snake[i + 1].X &&
                              snake[i - 1].Y + scale == snake[i + 1].Y &&
                              snake[i].X == snake[i + 1].X &&
                              snake[i].Y == snake[i - 1].Y) ||
                             (snake[i - 1].X - scale == snake[i + 1].X &&
                              snake[i - 1].Y - scale == snake[i + 1].Y &&
                              snake[i].X == snake[i - 1].X &&
                              snake[i].Y == snake[i + 1].Y))
                    {
                        image = Properties.Resources.RotateSegment;
                        image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    }
                    else if ((snake[i - 1].X - scale == snake[i + 1].X &&
                              snake[i - 1].Y + scale == snake[i + 1].Y &&
                              snake[i].X == snake[i - 1].X &&
                              snake[i].Y == snake[i + 1].Y) ||
                             (snake[i - 1].X + scale == snake[i + 1].X &&
                              snake[i - 1].Y - scale == snake[i + 1].Y &&
                              snake[i].X == snake[i + 1].X &&
                              snake[i].Y == snake[i - 1].Y))
                    {
                        image = Properties.Resources.RotateSegment;
                        image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    }
                    DrawImage(openGL, image, snake[i], scale);
                }
                image = Properties.Resources.EndSegment;
                if (snake[snake.Count - 2].X == snake[snake.Count - 1].X &&
                    snake[snake.Count - 2].Y - scale == snake[snake.Count - 1].Y)
                {
                    image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                }
                else if (snake[snake.Count - 2].Y == snake[snake.Count - 1].Y &&
                         snake[snake.Count - 2].X - scale == snake[snake.Count - 1].X)
                {
                    image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                }
                else if (snake[snake.Count - 2].Y == snake[snake.Count - 1].Y &&
                         snake[snake.Count - 2].X + scale == snake[snake.Count - 1].X)
                {
                    image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                }
                DrawImage(openGL, image, snake[snake.Count - 1], scale);
                toolStripStatusLabel1.Text = string.Format("Счет: {0}. Для прекращения игры нажмите клавишу 'ESC'.", score);
                if (pause)
                {
                    openGL.DrawText(10, 10, 1f, 0, 0, string.Empty, 16, "PAUSE");
                }
            }
        }

        void EndOfGame()
        {
            string text = string.Format("Ваш счет на уровне сложности '{0}': {1}.\nРекорд: {2}.", complexity.Level, score, complexity.Score);
            if (score > complexity.Score)
            {
                try
                {
                    Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
                    config.AppSettings.Settings.Remove(complexity.Key);
                    config.AppSettings.Settings.Add(complexity.Key, score.ToString());
                    config.Save(ConfigurationSaveMode.Modified);
                    complexity.Score = score;
                }
                catch (Exception)
                {

                }
                text += "\nНОВЫЙ РЕКОРД!!!";
            }
            EnableComponents(true);
            MessageBox.Show(text, "КОНЕЦ ИГРЫ!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            PaintScene();
        }

        bool CheckMatch(List<Coordinate> list1)
        {
            for (int i = 0; i < list1.Count - 1; i++)
            {
                for (int j = i + 1; j < list1.Count; j++)
                {
                    if (list1[i].Equals(list1[j]))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        bool CheckMatch(List<Coordinate> list1, List<Coordinate> list2)
        {
            for (int i = 0; i < list1.Count; i++)
            {
                for (int j = 0; j < list2.Count; j++)
                {
                    if (list1[i].Equals(list2[j]))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        bool CheckMatch(List<Coordinate> list1, Coordinate obj)
        {
            foreach (Coordinate item in list1)
            {
                if (item.Equals(obj))
                {
                    return true;
                }
            }
            return false;
        }

        void AddAppleAndBomb()
        {
            while (true)
            {
                apple = new Coordinate(random.Next(openGLControl1.Width / scale) * scale, random.Next(openGLControl1.Height / scale) * scale);
                bombs = new List<Coordinate>();
                for (int i = 0; i < complexity.BombCount; i++)
                {
                    bombs.Add(new Coordinate(random.Next(openGLControl1.Width / scale) * scale, random.Next(openGLControl1.Height / scale) * scale));
                }
                if (CheckMatch(bombs))
                    continue;
                else if (CheckMatch(snake, bombs))
                    continue;
                else if (CheckMatch(snake, apple))
                    continue;
                else if (CheckMatch(bombs, apple))
                    continue;
                else
                    break;
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void новаяИграToolStripMenuItem_Click(object sender, EventArgs e)
        {
            direction = Direction.Down;
            snake = new List<Coordinate>();
            snake.Add(new Coordinate(400, 400));
            snake.Add(new Coordinate(400, 400 + scale));
            snake.Add(new Coordinate(400, 400 + scale * 2));
            snake.Add(new Coordinate(400, 400 + scale * 3));
            AddAppleAndBomb();
            EnableComponents(false);
            openGLControl1.Focus();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Coordinate newSegment = snake[0].Copy();
            switch (direction)
            {
                case Direction.Up:
                    newSegment.Y += scale;
                    if (newSegment.Y > openGLControl1.Height - scale)
                    {
                        EndOfGame();
                    }
                    break;
                case Direction.Down:
                    newSegment.Y -= scale;
                    if (newSegment.Y <0)
                    {
                        EndOfGame();
                    }
                    break;
                case Direction.Left:
                    newSegment.X -= scale;
                    if (newSegment.X < 0)
                    {
                        EndOfGame();
                    }
                    break;
                case Direction.Right:
                    newSegment.X += scale;
                    if (newSegment.X > openGLControl1.Width - scale)
                    {
                        EndOfGame();
                    }
                    break;
            }
            if (!endOfGame)
            {
                snake.Insert(0, newSegment);
                if (newSegment.Equals(apple))
                {
                    AddAppleAndBomb();
                    score++;
                }
                else if (CheckMatch(bombs, newSegment) || CheckMatch(snake) || (openGLControl1.Width * openGLControl1.Height) / (scale * scale) - 4 - complexity.BombCount - snake.Count < 10)
                {
                    EndOfGame();
                }
                else snake.RemoveAt(snake.Count - 1);
                PaintScene();
            }
        }

        private void оченьПростоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            оченьПростоToolStripMenuItem.Checked = true;
            простоToolStripMenuItem.Checked = false;
            среднееToolStripMenuItem.Checked = false;
            сложноToolStripMenuItem.Checked = false;
            оченьСложноToolStripMenuItem.Checked = false;
            complexity = complexities[0];
            timer1.Interval = complexity.Interval;
        }

        private void простоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            оченьПростоToolStripMenuItem.Checked = false;
            простоToolStripMenuItem.Checked = true;
            среднееToolStripMenuItem.Checked = false;
            сложноToolStripMenuItem.Checked = false;
            оченьСложноToolStripMenuItem.Checked = false;
            complexity = complexities[1];
            timer1.Interval = complexity.Interval;
        }

        private void среднееToolStripMenuItem_Click(object sender, EventArgs e)
        {
            оченьПростоToolStripMenuItem.Checked = false;
            простоToolStripMenuItem.Checked = false;
            среднееToolStripMenuItem.Checked = true;
            сложноToolStripMenuItem.Checked = false;
            оченьСложноToolStripMenuItem.Checked = false;
            complexity = complexities[2];
            timer1.Interval = complexity.Interval;
        }

        private void сложноToolStripMenuItem_Click(object sender, EventArgs e)
        {
            оченьПростоToolStripMenuItem.Checked = false;
            простоToolStripMenuItem.Checked = false;
            среднееToolStripMenuItem.Checked = false;
            сложноToolStripMenuItem.Checked = true;
            оченьСложноToolStripMenuItem.Checked = false;
            complexity = complexities[3];
            timer1.Interval = complexity.Interval;
        }

        private void оченьСложноToolStripMenuItem_Click(object sender, EventArgs e)
        {
            оченьПростоToolStripMenuItem.Checked = false;
            простоToolStripMenuItem.Checked = false;
            среднееToolStripMenuItem.Checked = false;
            сложноToolStripMenuItem.Checked = false;
            оченьСложноToolStripMenuItem.Checked = true;
            complexity = complexities[4];
            timer1.Interval = complexity.Interval;
        }

        private void openGLControl1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Up:
                    if (direction != Direction.Down)
                    {
                        direction = Direction.Up;
                    }
                    break;
                case Keys.Down:
                    if (direction != Direction.Up)
                    {
                        direction = Direction.Down;
                    }
                    break;
                case Keys.Left:
                    if (direction != Direction.Right)
                    {
                        direction = Direction.Left;
                    }
                    break;
                case Keys.Right:
                    if (direction != Direction.Left)
                    {
                        direction = Direction.Right;
                    }
                    break;
                case Keys.P:
                    timer1.Enabled = pause;
                    pause = !pause;
                    PaintScene();
                    break;
                case Keys.Escape:
                    EnableComponents(true);
                    PaintScene();
                    break;
            }
        }

        private void рекордыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string text = string.Empty;
            foreach (Complexity item in complexities)
            {
                text += item.ToString() + "\n";
            }
            MessageBox.Show(text, "Рекорды.", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void openGLControl1_OpenGLDraw(object sender, RenderEventArgs args)
        {
            PaintScene();
        }

        private void openGLControl1_OpenGLInitialized(object sender, EventArgs e)
        {
            openGL = openGLControl1.OpenGL;
            openGL.Enable(OpenGL.GL_TEXTURE_2D);
            openGL.Enable(OpenGL.GL_DEPTH_TEST);
            openGL.DepthMask(1);
            openGL.DepthFunc(OpenGL.GL_LEQUAL);
        }

        private void openGLControl1_Resized(object sender, EventArgs e)
        {
            openGL.MatrixMode(OpenGL.GL_PROJECTION);
            openGL.LoadIdentity();
            openGL.Ortho2D(0, openGLControl1.Width, 0, openGLControl1.Height);
            openGL.Viewport(0, 0, openGLControl1.Width, openGLControl1.Height);
        }
    }
}
