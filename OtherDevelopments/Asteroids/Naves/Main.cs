using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Tao.OpenGl;
using ShadowEngine;
using ShadowEngine.OpenGL;
using ShadowEngine.Sound;

namespace Naves
{
    public partial class Main : Form
    {
        //handle del viewport
        uint hdc;
        Controller controladora = new Controller();
        static int moviendo;
        int score;
        bool started;
        int lives = 3;
        int level = 1;
        int levelCount;
        int count1, count2;
        bool pressed;
        bool pausado;

        public static int Moviendo
        {
            get { return Main.moviendo; }
            set { Main.moviendo = value; }
        }

        public Main()
        {
            InitializeComponent();
            hdc = (uint)pnlViewPort.Handle;
            string error = "";
            //Comando de inicializacion de la ventana grafica
            OpenGLControl.OpenGLInit(ref hdc, pnlViewPort.Width, pnlViewPort.Height, ref error);
            //inicia la posicion de la camara asi como define en angulo de perspectiva,etc etc
            controladora.Camara.SetPerspective();
            if (error != "")
            {
                MessageBox.Show("Something happened");
            }

            #region lights

            //Configuracion de las luces
            float[] materialAmbient = { 0.5F, 0.5F, 0.5F, 1.0F };
            float[] materialDiffuse = { 1f, 1f, 1f, 1.0f };
            float[] materialSpecular = { 1.0F, 1.0F, 1.0F, 1.0F };
            //brillo del material
            float[] materialShininess = { 1.0F };
            //posicion de la luz
            float[] ambientLightPosition = { 10F, -10F, 10F, 1.0F };
            // intensidad de la luz en RGB
            float[] lightAmbient = { 0.85F, 0.85F, 0.85F, 1.0F };

            Lighting.MaterialAmbient = materialAmbient;
            Lighting.MaterialDiffuse = materialDiffuse;
            Lighting.MaterialSpecular = materialSpecular;
            Lighting.MaterialShininess = materialShininess;
            Lighting.AmbientLightPosition = ambientLightPosition;
            Lighting.LightAmbient = lightAmbient;

            #endregion

            //Habilita las luces
            Lighting.SetupLighting();
            //cargar texturas
            ContentManager.SetModelList("modelos\\");
            ContentManager.LoadModels();
            ContentManager.SetTextureList("texturas\\");
            ContentManager.LoadTextures();
            //AudioPlayback.SoundDir = "sonidos\\";
            //AudioPlayback.LoadSounds();
            controladora.CreateObjects();
            //Color de fondo
            Gl.glClearColor(0, 0, 0, 1);//red green blue alpha 
            Gl.glShadeModel(Gl.GL_SMOOTH);  
        }

        private void tmrPaint_Tick(object sender, EventArgs e)
        {
            // limpia opengl
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            //dibuja la escena
            controladora.DrawScene();
            //cambia los buffer
            Winapi.SwapBuffers(hdc);
            //termina de pintar
            Gl.glFlush();

            if (started)
            {
                score += 1;
                count1++;
                count2++;
                levelCount++;
                if (levelCount == 450)
                {
                    level++;
                    AsteroidGenerator.GenerateAsteroid(35, false);
                    lblLevel.Text = level.ToString();
                    levelCount = 0;
                }
                if (count1 == 4)
                {
                    lblScore.Text = score.ToString();
                    count1 = 0;
                    lives = controladora.Nave.Vidas;
                }
                if (count2 == 20)
                {
                    ShowLife(lives);
                    count2 = 0;
                    if (lives == 0)
                    {
                        started = false; 
                        MessageBox.Show("Game Over");
                        controladora.ResetGame();
                        score = 0;
                        level = 1;
                        lives = 3;
                        started = true;
                        count1 = 0;
                        count2 = 0;
                        lblLevel.Text = level.ToString();   
                    }
                }
            }
        }

        void ShowLife(int vidas)
        {
            if (vidas == 3)
            {
                picVida1.Visible = true;
                picVida2.Visible = true;
                picVida3.Visible = true;
            }
            if (vidas == 2)
            {
                picVida1.Visible = true;
                picVida2.Visible = true;
                picVida3.Visible = false;
            }
            if (vidas == 1)
            {
                picVida1.Visible = true;
                picVida2.Visible = false;
                picVida3.Visible = false;
            }
            if (vidas == 0)
            {
                picVida1.Visible = false;
                picVida2.Visible = false;
                picVida3.Visible = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (started == false)
            {
                controladora.BeginGame();
                started = true;
                //AudioPlayback.PlayLoop("fondo.mp3");
                btnComenzar.Text = "Pausar";
            }
            else
            {
                if (pausado)
                {
                    pausado = false;
                    tmrPaint.Enabled = true;
                    btnComenzar.Text = "Pausar";
                    //AudioPlayback.ResumeAllSounds();
                }
                else
                {
                    pausado = true;
                    tmrPaint.Enabled = false;
                    btnComenzar.Text = "Resumir";
                    //AudioPlayback.StopAllSounds();
                }
            }
        }

        private void btnReiniciar_Click(object sender, EventArgs e)
        {
            controladora.ResetGame();
            score = 0;
            if (pausado)
            {
                pausado = false;
                tmrPaint.Enabled = true;
                btnComenzar.Text = "Pausar";
                AudioPlayback.StopAllSounds();
            }
        }

        private void Main_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
            {
                moviendo = -1;
            }
            if (e.KeyCode == Keys.D)
            {
                moviendo = 1;
            }
            if (e.KeyCode == Keys.W)
            {
                moviendo = 2;
            }
            if (e.KeyCode == Keys.S)
            {
                moviendo = -2;
            }
            if (e.KeyCode == Keys.A || e.KeyCode == Keys.D || e.KeyCode == Keys.W || e.KeyCode == Keys.S)
            {
                if (pressed == false)
                {
                   // AudioPlayback.PlayOne("mover.wav");
                }
            }
            pressed = true;
        }

        private void Main_KeyUp(object sender, KeyEventArgs e)
        {
            moviendo = 0;
            pressed = false; 
        }
    }
}
