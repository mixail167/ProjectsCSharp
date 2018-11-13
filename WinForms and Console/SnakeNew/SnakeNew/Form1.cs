using SharpGL;
using SharpGL.SceneGraph.Assets;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;

namespace SnakeNew
{
    enum Direction { Up, Down, Left, Right };

    public partial class Form1 : Form
    {
        bool pause;
        bool endOfGame;
        uint score;
        const int scale = 10;
        Direction direction;
        List<Coordinate> snake;
        List<Coordinate> bombs;
        Coordinate apple;
        Random random;
        Complexity complexity;
        List<Complexity> complexities;
        OpenGL openGL;
        Texture[] textures;
        bool run;

        public Form1()
        {
            InitializeComponent();
            complexities = new List<Complexity>();
            complexities.Add(new Complexity("record0", "Очень просто", 1, 150));
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

        void DrawImage(OpenGL openGL, Texture texture, Coordinate coord, int scale)
        {
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
            openGL.Color(1f, 1f, 1f, 1f);
            if (run)
            {
                DrawImage(openGL, textures[0], apple, scale);
                foreach (Coordinate item in bombs)
                {
                    DrawImage(openGL, textures[1], item, scale);
                }
                switch (direction)
                {
                    case Direction.Up:
                        DrawImage(openGL, textures[2], snake[0], scale);
                        break;
                    case Direction.Down:
                        DrawImage(openGL, textures[3], snake[0], scale);
                        break;
                    case Direction.Left:
                        DrawImage(openGL, textures[4], snake[0], scale);
                        break;
                    case Direction.Right:
                        DrawImage(openGL, textures[5], snake[0], scale);
                        break;
                }
                for (int i = 1; i < snake.Count - 1; i++)
                {
                    if (snake[i - 1].X == snake[i + 1].X && (snake[i - 1].Y + 2 * scale == snake[i + 1].Y || snake[i - 1].Y - 2 * scale == snake[i + 1].Y))
                    {
                        DrawImage(openGL, textures[6], snake[i], scale);
                    }
                    else if (snake[i - 1].Y == snake[i + 1].Y && (snake[i - 1].X + 2 * scale == snake[i + 1].X || snake[i - 1].X - 2 * scale == snake[i + 1].X))
                    {
                        DrawImage(openGL, textures[7], snake[i], scale);
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
                        DrawImage(openGL, textures[8], snake[i], scale);
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
                        DrawImage(openGL, textures[9], snake[i], scale);
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
                        DrawImage(openGL, textures[10], snake[i], scale);
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
                        DrawImage(openGL, textures[11], snake[i], scale);
                    }
                }
                if (snake[snake.Count - 2].X == snake[snake.Count - 1].X &&
                    snake[snake.Count - 2].Y - scale == snake[snake.Count - 1].Y)
                {
                    DrawImage(openGL, textures[12], snake[snake.Count - 1], scale);
                }
                else if (snake[snake.Count - 2].Y == snake[snake.Count - 1].Y &&
                         snake[snake.Count - 2].X - scale == snake[snake.Count - 1].X)
                {
                    DrawImage(openGL, textures[13], snake[snake.Count - 1], scale);
                }
                else if (snake[snake.Count - 2].Y == snake[snake.Count - 1].Y &&
                         snake[snake.Count - 2].X + scale == snake[snake.Count - 1].X)
                {
                    DrawImage(openGL, textures[14], snake[snake.Count - 1], scale);
                }
                else
                {
                    DrawImage(openGL, textures[15], snake[snake.Count - 1], scale);
                }
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
                    if (newSegment.Y < 0)
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
                    break;
                case Keys.Escape:
                    EndOfGame();
                    EnableComponents(true);
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
            openGL.ClearColor(1f, 1f, 1f, 1f);
            textures = new Texture[16] { new Texture(), new Texture(), new Texture(), new Texture(), new Texture(), new Texture(), new Texture(), new Texture(), new Texture(), new Texture(), new Texture(), new Texture(), new Texture(), new Texture(), new Texture(), new Texture() };
            textures[0].Create(openGL, Properties.Resources.Apple);
            textures[1].Create(openGL, Properties.Resources.bomb);

            Image image = Properties.Resources.StartSegment;
            textures[2].Create(openGL, (Bitmap)image);
            image = Properties.Resources.StartSegment;
            image.RotateFlip(RotateFlipType.Rotate180FlipNone);
            textures[3].Create(openGL, (Bitmap)image);
            image = Properties.Resources.StartSegment;
            image.RotateFlip(RotateFlipType.Rotate270FlipNone);
            textures[4].Create(openGL, (Bitmap)image);
            image = Properties.Resources.StartSegment;
            image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            textures[5].Create(openGL, (Bitmap)image);

            image = Properties.Resources.DefaultSegment;
            textures[6].Create(openGL, (Bitmap)image);
            image = Properties.Resources.DefaultSegment;
            image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            textures[7].Create(openGL, (Bitmap)image);

            image = Properties.Resources.RotateSegment;
            image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            textures[8].Create(openGL, (Bitmap)image);
            image = Properties.Resources.RotateSegment;
            textures[9].Create(openGL, (Bitmap)image);
            image = Properties.Resources.RotateSegment;
            image.RotateFlip(RotateFlipType.Rotate270FlipNone);
            textures[10].Create(openGL, (Bitmap)image);
            image = Properties.Resources.RotateSegment;
            image.RotateFlip(RotateFlipType.Rotate180FlipNone);
            textures[11].Create(openGL, (Bitmap)image);

            image = Properties.Resources.EndSegment;
            image.RotateFlip(RotateFlipType.Rotate180FlipNone);
            textures[12].Create(openGL, (Bitmap)image);
            image = Properties.Resources.EndSegment;
            image.RotateFlip(RotateFlipType.Rotate270FlipNone);
            textures[13].Create(openGL, (Bitmap)image);
            image = Properties.Resources.EndSegment;
            image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            textures[14].Create(openGL, (Bitmap)image);
            image = Properties.Resources.EndSegment;
            textures[15].Create(openGL, (Bitmap)image);
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
