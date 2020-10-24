using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace Visualisation_Labyrynth
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LabArray.ArrayReader(AppDomain.CurrentDomain.BaseDirectory + "Lab.txt");
            labyrynth = new Labyrynth();
            MouseButtonIsPressed = false;
        }

        Labyrynth labyrynth;
        bool MouseButtonIsPressed;
        Point CursorLocation;
        private void buttonFindPath_Click(object sender, EventArgs e)
        {
            LabArray.SaveLab();
            LabArray.ArrayWidthFinder();
            LabArray.ArrayHeightFinder();
            if (comboBoxAlghorythms.SelectedItem == null)
                MessageBox.Show("Выберите алгоритм поиска кратчайшего пути.", "Ошибка");

            if (comboBoxAlghorythms.SelectedIndex == 0)
            {
                AStarAlgorythm star = new AStarAlgorythm();
                star.FindPath(LabArray.Height - 2, 0, 1, LabArray.Width - 1);
                Lab.Refresh();
            }

            if (comboBoxAlghorythms.SelectedIndex == 1)
            {
                DijkstraAlgorythm dextr = new DijkstraAlgorythm();
                dextr.FindPath(LabArray.Height - 2, 0, 1, LabArray.Width - 1);
                Lab.Refresh();
            }

            if (comboBoxAlghorythms.SelectedIndex == 2)
            {
                WaveAlgorythm wave = new WaveAlgorythm();
                wave.FindPath(LabArray.Height - 2, 0, 1, LabArray.Width - 1);
                Lab.Refresh();
            }

            LabArray.ArrayReader(AppDomain.CurrentDomain.BaseDirectory + "Lab.txt");
        }

        private void buttonRestart_Click(object sender, EventArgs e)
        {
            LabArray.SaveLab();
            LabArray.ArrayReader(AppDomain.CurrentDomain.BaseDirectory + "Lab.txt");
            Lab.Refresh();

        }

        private void Lab_Click(object sender, EventArgs e)
        {
            LabArray.PointToArray(Lab.PointToClient(MousePosition).X, Lab.PointToClient(MousePosition).Y);
            Lab.Refresh();
            MouseButtonIsPressed = false;
        }
        private void Lab_Paint(object sender, PaintEventArgs e)
        {
            labyrynth.DrawLab(this, e);
        }

        private void buttonBuildNewLab_Click(object sender, EventArgs e)
        {
            if (textBoxNewWidth.Text == "")
                MessageBox.Show("Вы не ввели значение ширины нового лабиринта.", "Ошибка");
            else
            if (textBoxNewHeight.Text == "")
                MessageBox.Show("Вы не ввели значение высоты нового лабиринта.", "Ошибка");
            else if (LabArray.IsDigit(textBoxNewHeight.Text) && LabArray.IsDigit(textBoxNewWidth.Text))
                LabArray.CreateNewLab(int.Parse(textBoxNewHeight.Text) - 1, int.Parse(textBoxNewWidth.Text) - 1);
            else MessageBox.Show("Введенное вами значение не соответствует числовому формату.", "Ошибка");
            LabArray.ArrayReader(AppDomain.CurrentDomain.BaseDirectory + "Lab.txt");
            Lab.Refresh();
        }

        private void buttonClearWalls_Click(object sender, EventArgs e)
        {
            LabArray.ArrayWidthFinder();
            LabArray.ArrayHeightFinder();
            LabArray.CreateNewLab(LabArray.Height - 1, LabArray.Width - 1);
            LabArray.ArrayReader(AppDomain.CurrentDomain.BaseDirectory + "Lab.txt");
            Lab.Refresh();
        }

        private void Lab_MouseDown(object sender, MouseEventArgs e)
        {
            MouseButtonIsPressed = true;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            LabArray.SaveLab();
        }

        private void Lab_MouseMove(object sender, MouseEventArgs e)
        {
            if (MouseButtonIsPressed)
            {
                double x = Lab.PointToClient(MousePosition).X;
                double y = Lab.PointToClient(MousePosition).Y;
                x = x / (500 / LabArray.Width);
                y = y / (500 / LabArray.Height);
                x = Math.Floor(x);
                y = Math.Floor(y);

                if (CursorLocation.X != int.Parse(x.ToString()) || CursorLocation.Y != int.Parse(y.ToString()))
                {
                    LabArray.PointToArray(Lab.PointToClient(MousePosition).X, Lab.PointToClient(MousePosition).Y);
                    CursorLocation.X = int.Parse(x.ToString());
                    CursorLocation.Y = int.Parse(y.ToString());
                    Lab.Refresh();
                }
            }
            else return;
        }

        private void Lab_MouseUp(object sender, MouseEventArgs e)
        {
            MouseButtonIsPressed = false;
        }
    }
    public static class LabArray
    {
        public static int[,] Lab { get; set; }   // Массив для работы с лабиринтом
        public static int Width { get; private set; }    // Ширина массива
        public static int Height { get; private set; }   // Высота массива

        public static int[,] ArrayReader(string path)
        {
            Width = ArrayWidthFinder();
            Height = ArrayHeightFinder();
            Lab = new int[ArrayHeightFinder(), ArrayWidthFinder()];

            StreamReader sr = new StreamReader(path);
            try
            {
                int i = 0;
                string s = "";
                while (sr.Peek() != 0)
                {
                    s = sr.ReadLine();
                    for (int j = 0; j < s.Length; j++)
                    {
                        Lab[i, j] = int.Parse(s[j].ToString());
                    }
                    i++;
                }
            }
            catch (NullReferenceException e) { }
            catch (ArgumentNullException e) { MessageBox.Show(e.Message); }
            catch (FileNotFoundException e) { MessageBox.Show(e.Message, e.FileName); }
            catch (DirectoryNotFoundException e) { MessageBox.Show(e.Message); }
            catch (IOException e) { MessageBox.Show(e.Message); }
            sr.Close();
            return Lab;
        }
        public static void PointToArray(double x, double y)
        {
            x = x / (500 / Width);
            y = y / (500 / Height);
            x = Math.Floor(x);
            y = Math.Floor(y);
            int i = int.Parse(y.ToString()), j = int.Parse(x.ToString());
            if (i != 0 && j != 0 && i != Height - 1 && j != Width - 1)
            {
                if (Lab[i, j] == 0)
                    Lab[i, j] = 1;
                else if (Lab[i, j] == 1)
                    Lab[i, j] = 0;
                else if (Lab[i, j] == 2)
                    MessageBox.Show("Невозможно изменение точки старта.");
                else if (Lab[i, j] == 3)
                    MessageBox.Show("Невозможно изменение точки финиша.");
            }
            else
            {
                if (Lab[i, j] == 2)
                    MessageBox.Show("Невозможно изменение точки старта.");
                else if (Lab[i, j] == 3)
                    MessageBox.Show("Невозможно изменение точки финиша.");
                else
                    MessageBox.Show("Невозможно изменение крайних стен лабиринта.");
            }
        }

        public static int ArrayWidthFinder()
        {
            try
            {
                StreamReader str = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "Lab.txt");
                string s = str.ReadLine();
                str.Close();
                return s.Length;
            }
            catch (FileNotFoundException e) { MessageBox.Show(e.Message, e.FileName); }
            return 100;
        }

        public static int ArrayHeightFinder()
        {
            StreamReader str = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "Lab.txt");
            string s;
            Height = 0;
            while ((s = str.ReadLine()) != null)
                Height++;
            str.Close();
            return Height;
        }
        public static void SaveLab()
        {
            StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "Lab.txt");
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    sw.Write(Lab[i, j].ToString());
                }
                sw.WriteLine();
            }
            sw.Close();

        }
        public static void CreateNewLab(int height, int width)
        {
            StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "Lab.txt");
            for (int i = 0; i <= height; i++)
            {
                for (int j = 0; j <= width; j++)
                {
                    if (i == height - 1 && j == 0)
                        sw.Write("2");
                    else
                    if (i == 1 && j == width)
                        sw.Write("3");
                    else
                    if (i == 0 || j == 0 || i == height || j == width)
                        sw.Write("1");
                    else
                        sw.Write("0");
                }
                sw.WriteLine();
            }
            sw.Close();
        }

        public static int MinOfFour(int first, int second, int third, int fourth)
        {
            int min = int.MaxValue;

            if (first != -2)
                if (min > first)
                    min = first;

            if (second != -2)
                if (min > second)
                    min = second;

            if (third != -2)
                if (min > third)
                    min = third;

            if (fourth != -2)
                if (min > fourth)
                    min = fourth;

            return min;
        }
        public static bool IsDigit(string s)
        {
            char[] str = s.ToCharArray();
            for (int i = 0; i < str.Length; i++)
                if (!char.IsDigit(str[i]))
                    return false;
            return true;
        }

    }
    public class Labyrynth
    {
        public int[,] Lab { get; set; }           // Массив для работы с лабиринтом
        public int Width { get; private set; }    // Ширина лабиринта
        public int Height { get; private set; }   // Высота лабиринта
        public int NumberOfCellsInWidth { get; private set; }    // Количество ячеек в ширину
        public int NumberOfCellsInHeight { get; private set; }   // Количество ячеек в высоту

        public Labyrynth()
        {
            Width = 500;
            Height = 500;
            Lab = new int[LabArray.ArrayHeightFinder(), LabArray.ArrayWidthFinder()];
            NumberOfCellsInWidth = LabArray.ArrayHeightFinder();
            NumberOfCellsInHeight = LabArray.ArrayWidthFinder();
        }
        public void DrawLab(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.LightSlateGray);

            Cell cell = new Cell(0, 0);

            for (int i = 0; i < LabArray.Height; i++)
            {
                for (int j = 0; j < LabArray.Width; j++)
                {
                    if (LabArray.Lab[i, j] == 1)
                    {
                        cell.Color = Color.Black;
                        cell.DrawCell(this, e);
                        cell.X += cell.Width;
                    }
                    if (LabArray.Lab[i, j] == 0)
                    {
                        cell.Color = Color.White;
                        cell.DrawCell(this, e);
                        cell.X += cell.Width;
                    }
                    if (LabArray.Lab[i, j] == 2)
                    {
                        cell.Color = Color.LimeGreen;
                        cell.DrawCell(this, e);
                        cell.X += cell.Width;
                    }
                    if (LabArray.Lab[i, j] == 3)
                    {
                        cell.Color = Color.Red;
                        cell.DrawCell(this, e);
                        cell.X += cell.Width;
                    }
                    if (LabArray.Lab[i, j] == 4)
                    {
                        cell.Color = Color.Yellow;
                        cell.DrawCell(this, e);
                        cell.X += cell.Width;
                    }
                    if (LabArray.Lab[i, j] != 0 && LabArray.Lab[i, j] != 1 && LabArray.Lab[i, j] != 2 && LabArray.Lab[i, j] != 3 && LabArray.Lab[i, j] != 4)
                        MessageBox.Show("В исходном файле лабиринта содержатся невенрые данные \n" +
                            "Лабиринт должен быть составлен из цифр: 0, 1. \n" +
                            "Используйте цифры 2 и 3 для указания начала и конца соответственно.", "Ошибка");
                }
                cell.Y += cell.Height;
                cell.X = 0;
            }

            for (int i = 0; i < LabArray.Height; i++)
            {
                for (int j = 0; j < LabArray.Width; j++)
                {
                    e.Graphics.DrawLine(pen, j * cell.Width, 0, j * cell.Width, 500);
                    e.Graphics.DrawLine(pen, 0, i * cell.Height, 500, i * cell.Height);
                }
            }
        }
       
    }
    public class Cell // Ячейка лабиринта
    {
        public int Height { get; set; } // Высота ячейки
        public int Width { get; set; }  // Ширина ячейки
        // Координаты верхнего левого угла ячейки
        public int X { get; set; }
        public int Y { get; set; }
        public Color Color { get; set; }
        public Cell(int x, int y)
        {
            Height = 500 / LabArray.ArrayHeightFinder();
            Width = 500 / LabArray.ArrayWidthFinder();
            X = x;
            Y = y;
            Color = Color.White;
        }
        public void DrawCell(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color);
            Brush brush = new SolidBrush(Color);
            Rectangle rect = new Rectangle();
            rect.Width = Width;
            rect.Height = Height;
            rect.X = X;
            rect.Y = Y;
            e.Graphics.DrawRectangle(pen, rect);
            e.Graphics.FillRectangle(brush, rect);
        }
    }
    public abstract class ShortestWay
    {
        private List<int> listX { get; set; }
        private List<int> listY { get; set; }
        public void FindPath() { }
        private void AddPathToLab() { }

    }
    public class AStarAlgorythm : ShortestWay
    {
        private List<int> listX { get; set; }
        private List<int> listY { get; set; }
        public AStarAlgorythm()
        {
            listX = new List<int>();
            listY = new List<int>();
        }

        public void FindPath(int startHeight, int startWidth, int endHeight, int endWidth)
        {
            LabArray.ArrayReader(AppDomain.CurrentDomain.BaseDirectory + "Lab.txt");
            int y, x = 0;
            int step = 0;

            int[,] heuristic = new int[LabArray.Height, LabArray.Width];
            int[,] distance = new int[LabArray.Height, LabArray.Width];

            for (x = 0; x < LabArray.Height; x++)
                for (y = 0; y < LabArray.Width; y++)
                {
                    if (LabArray.Lab[x, y] == 1)
                        heuristic[x, y] = -2;
                    else
                    if (LabArray.Lab[x, y] == 3)
                        heuristic[x, y] = 0;
                    else heuristic[x, y] = -1;
                }


            while (heuristic[startHeight, startWidth] == -1 && step < LabArray.Width * LabArray.Height)
            {
                for (x = 0; x < LabArray.Height; x++)
                {
                    for (y = 0; y < LabArray.Width; y++)
                    {
                        if (heuristic[x, y] == step)
                        {
                            if (x != 0)
                            {
                                if (heuristic[x - 1, y] != -2 && heuristic[x - 1, y] == -1)
                                    heuristic[x - 1, y] = step + 1;
                                else
                                if (x != 1)
                                    if (heuristic[x - 1, y] == -2 && heuristic[x - 1, y] == -1)
                                        heuristic[x - 2, y] = step + 1;
                            }

                            if (y != 0)
                            {
                                if (heuristic[x, y - 1] != -2 && heuristic[x, y - 1] == -1)
                                    heuristic[x, y - 1] = step + 1;
                                else
                                if (y != 1)
                                    if (heuristic[x, y - 1] == -2 && heuristic[x, y - 1] == -1)
                                        heuristic[x, y - 2] = step + 1;
                            }

                            if (x != LabArray.Height - 1)
                                if (heuristic[x + 1, y] != -2 && heuristic[x + 1, y] == -1)
                                    heuristic[x + 1, y] = step + 1;
                                else
                            if (x != LabArray.Height - 2)
                                    if (heuristic[x + 1, y] == -2 && heuristic[x + 1, y] == -1)
                                        heuristic[x + 2, y] = step + 1;

                            if (y != LabArray.Width - 1)
                                if (heuristic[x, y + 1] != -2 && heuristic[x, y + 1] == -1)
                                    heuristic[x, y + 1] = step + 1;
                                else
                            if (y != LabArray.Width - 2)
                                    if (heuristic[x, y + 1] == -2 && heuristic[x, y + 1] == -1)
                                        heuristic[x, y + 2] = step + 1;
                        }
                    }
                }
                step++;
            }

            for (x = 0; x < LabArray.Height; x++)
                for (y = 0; y < LabArray.Width; y++)
                {
                    if (LabArray.Lab[x, y] == 1)
                        distance[x, y] = -2;
                    else
                    if (LabArray.Lab[x, y] == 2)
                        distance[x, y] = 0;
                    else distance[x, y] = -1;
                }

            step = 0;
            int[,] map = new int[LabArray.Height, LabArray.Width];

            for (x = 0; x < LabArray.Height; x++)
                for (y = 0; y < LabArray.Width; y++)
                {
                    if (distance[x, y] != -2)
                        map[x, y] = heuristic[x, y] + 1;
                    else
                        map[x, y] = -2;
                }

            if (map[endHeight, endWidth] != -1)
            {
                x = startHeight;
                y = startWidth;
                listX.Add(startHeight);
                listY.Add(startWidth);
                bool temp = true;
                int min = 0;
                while (x != endHeight || y != endWidth && temp == true)
                {
                    if (x != LabArray.Height && y != startWidth && x != 0 && y != 0)
                        min = LabArray.MinOfFour(map[x - 1, y], map[x + 1, y], map[x, y - 1], map[x, y + 1]);

                    if (x == startHeight && y == startWidth)
                        min = map[startHeight, startWidth] - 1;

                    if (x != 0)
                        if (map[x - 1, y] == min)
                        {
                            map[x, y] = 100;
                            x -= 1;
                            listX.Add(x);
                            listY.Add(y);
                            if (x != 0)
                                min = LabArray.MinOfFour(map[x - 1, y], map[x + 1, y], map[x, y - 1], map[x, y + 1]);
                        }


                    if (y != startWidth)
                        if (map[x, y - 1] == min)
                        {
                            map[x, y] = 100;
                            y -= 1;
                            listX.Add(x);
                            listY.Add(y);
                            if (y != startWidth)
                                min = LabArray.MinOfFour(map[x - 1, y], map[x + 1, y], map[x, y - 1], map[x, y + 1]);
                        }

                    if (x != LabArray.Height)
                        if (map[x + 1, y] == min)
                        {
                            map[x, y] = 100;
                            x += 1;
                            listX.Add(x);
                            listY.Add(y);
                            if (x != LabArray.Height)
                                min = LabArray.MinOfFour(map[x - 1, y], map[x + 1, y], map[x, y - 1], map[x, y + 1]);
                        }

                    if (y != endWidth)
                    {
                        if (map[x, y + 1] == min)
                        {
                            map[x, y] = 100;
                            y += 1;
                            listX.Add(x);
                            listY.Add(y);
                            if (y != endWidth)
                                min = LabArray.MinOfFour(map[x - 1, y], map[x + 1, y], map[x, y - 1], map[x, y + 1]);
                        }
                    }

                    if (step > LabArray.Height * LabArray.Width * 2)
                    {
                        listX.Clear();
                        temp = false;
                        break;
                    }
                    step++;
                }
            }
            AddPathToLab();
        }

        private void AddPathToLab()
        {
            int count = 0;
            int[] masX = new int[listX.Count];
            if (listX.Count == 0)
                MessageBox.Show("Выставленные препятствия блокируют путь", "Ошибка");
            int[,] masXY = new int[listX.Count, listY.Count];
            foreach (int x in listX)
            {
                masX[count] = x;
                count++;
            }
            count = 0;
            int[] masY = new int[listY.Count];
            foreach (int y in listY)
            {
                masY[count] = y;
                count++;
            }
            int i = 0;
            while (i < masX.Length)
            {
                LabArray.Lab[masX[i], masY[i]] = 4;
                i++;
            }
        }
    }
    public class DijkstraAlgorythm : ShortestWay
    {
        private List<int> listX { get; set; }
        private List<int> listY { get; set; }
        public DijkstraAlgorythm()
        {
            listX = new List<int>();
            listY = new List<int>();
        }
        public void FindPath(int startHeight, int startWidth, int endHeight, int endWidth)
        {
            LabArray.ArrayReader(AppDomain.CurrentDomain.BaseDirectory + "Lab.txt");
            int y, x = 0;
            int step = 0;

            int[,] distance = new int[LabArray.Height, LabArray.Width];
            int [,] value = new int[LabArray.Height, LabArray.Width];

            for (x = 0; x < LabArray.Height; x++)
                for (y = 0; y < LabArray.Width; y++)
                {
                    if (LabArray.Lab[x, y] == 1)
                        distance[x, y] = -2;
                    else
                    if (LabArray.Lab[x, y] == 2)
                        distance[x, y] = 0;
                    else distance[x, y] = -1;
                }

            for (x = 0; x < LabArray.Height; x++)
                for (y = 0; y < LabArray.Width; y++)
                {
                    if (LabArray.Lab[x, y] == 1)
                        value[x, y] = -2;
                    else
                    if (LabArray.Lab[x, y] == 2)
                        value[x, y] = 0;
                    else value[x, y] = int.MaxValue;
                }

            while (distance[endHeight, endWidth] == -1 && step < LabArray.Width * LabArray.Height)
            {
                for (x = 0; x < LabArray.Height; x++)
                {
                    for (y = 0; y < LabArray.Width; y++)
                    {
                        if (distance[x, y] == step)
                        {
                            if (x != 0)
                            {
                                if (distance[x - 1, y] != -2 && distance[x - 1, y] == -1)
                                {
                                    distance[x - 1, y] = step + 1;
                                    value[x - 1, y] = step + 1;
                                }
                            }
                            if (y != 0)
                            {
                                if (distance[x, y - 1] != -2 && distance[x, y - 1] == -1)
                                {
                                    distance[x, y - 1] = step + 1;
                                    value[x, y - 1] = step + 1;
                                }
                            }

                            if (distance[x + 1, y] != -2 && distance[x + 1, y] == -1)
                            {
                                distance[x + 1, y] = step + 1;
                                value[x + 1, y] = step + 1;
                            }

                            if (distance[x, y + 1] != -2 && distance[x, y + 1] == -1)
                            {
                                distance[x, y + 1] = step + 1;
                                value[x, y + 1] = step + 1;
                            }
                        }
                    }
                }
                step++;
            }

            int[,] distanceTemp = new int[LabArray.Height, LabArray.Width];

            for (x = 0; x < LabArray.Height; x++)
                for (y = 0; y < LabArray.Width; y++)
                {
                        distanceTemp[x, y] = distance[x,y];
                }

            if (distanceTemp[endHeight, endWidth] != -1)
            {
                x = endHeight;
                y = endWidth;
                bool temp = true;
                int min = 0;
                while (x != startHeight || y != startWidth && temp == true)
                {
                    if (x != LabArray.Height && y != LabArray.Width - 1 && x != 0 && y != 0)
                        min = LabArray.MinOfFour(distanceTemp[x - 1, y], distanceTemp[x + 1, y], distanceTemp[x, y - 1], distanceTemp[x, y + 1]);

                    if (x == endHeight && y == endWidth)
                        min = distanceTemp[endHeight, endWidth] - 1;

                    if (x != 0)
                        if (distanceTemp[x - 1, y] == min)
                        {
                            distanceTemp[x, y] = 100;
                            x -= 1;
                            if (min < value[x, y])
                                value[x, y] = min;
                            if (x != 0)
                                min = LabArray.MinOfFour(distanceTemp[x - 1, y], distanceTemp[x + 1, y], distanceTemp[x, y - 1], distanceTemp[x, y + 1]);
                        }

                    if (y != startWidth)
                        if (distanceTemp[x, y - 1] == min)
                        {
                            distanceTemp[x, y] = 100;
                            y -= 1;
                            if (min < value[x, y])
                                value[x, y] = min;
                            if (y != startWidth)
                                min = LabArray.MinOfFour(distanceTemp[x - 1, y], distanceTemp[x + 1, y], distanceTemp[x, y - 1], distanceTemp[x, y + 1]);
                        }

                    if (x != LabArray.Height)
                        if (distanceTemp[x + 1, y] == min)
                        {
                            distanceTemp[x, y] = 100;
                            x += 1;
                            if (min < value[x, y])
                                value[x, y] = min;
                            if (x != LabArray.Height)
                                min = LabArray.MinOfFour(distanceTemp[x - 1, y], distanceTemp[x + 1, y], distanceTemp[x, y - 1], distanceTemp[x, y + 1]);
                        }

                    if (y != endWidth)
                    {
                        if (distanceTemp[x, y + 1] == min)
                        {
                            distanceTemp[x, y] = 100;
                            y += 1;
                            if (min < value[x, y])
                                value[x, y] = min;
                            if (y != endWidth)
                                min = LabArray.MinOfFour(distanceTemp[x - 1, y], distanceTemp[x + 1, y], distanceTemp[x, y - 1], distanceTemp[x, y + 1]);
                        }
                    }

                    if (step > LabArray.Height * LabArray.Width * 2)
                    {
                        temp = false;
                        break;
                    }
                    step++;
                }
            }

            step = 0;

            if (value[endHeight, endWidth] != -1)
            {
                x = endHeight;
                y = endWidth;
                listX.Add(endHeight);
                listY.Add(endWidth);
                bool temp = true;
                int min = 0;
                while (x != startHeight || y != startWidth && temp == true)
                {
                    if (x != LabArray.Height && y != LabArray.Width - 1 && x != 0 && y != 0)
                        min = LabArray.MinOfFour(value[x - 1, y], value[x + 1, y], value[x, y - 1], value[x, y + 1]);

                    if (x == endHeight && y == endWidth)
                        min = value[endHeight, endWidth] - 1;

                    if (x != 0)
                        if (value[x - 1, y] == min)
                        {
                            value[x, y] = 100;
                            x -= 1;
                            listX.Add(x);
                            listY.Add(y);
                            if (x != 0)
                                min = LabArray.MinOfFour(value[x - 1, y], value[x + 1, y], value[x, y - 1], value[x, y + 1]);
                        }


                    if (y != startWidth)
                        if (value[x, y - 1] == min)
                        {
                            value[x, y] = 100;
                            y -= 1;
                            listX.Add(x);
                            listY.Add(y);
                            if (y != startWidth)
                                min = LabArray.MinOfFour(value[x - 1, y], value[x + 1, y], value[x, y - 1], value[x, y + 1]);
                        }

                    if (x != LabArray.Height)
                        if (value[x + 1, y] == min)
                        {
                            value[x, y] = 100;
                            x += 1;
                            listX.Add(x);
                            listY.Add(y);
                            if (x != LabArray.Height)
                                min = LabArray.MinOfFour(value[x - 1, y], value[x + 1, y], value[x, y - 1], value[x, y + 1]);
                        }

                    if (y != endWidth)
                    {
                        if (value[x, y + 1] == min)
                        {
                            value[x, y] = 100;
                            y += 1;
                            listX.Add(x);
                            listY.Add(y);
                            if (y != endWidth)
                                min = LabArray.MinOfFour(value[x - 1, y], value[x + 1, y], value[x, y - 1], value[x, y + 1]);
                        }
                    }

                    if (step > LabArray.Height * LabArray.Width * 2)
                    {
                        listX.Clear();
                        temp = false;
                        break;
                    }
                    step++;
                }
            }
            AddPathToLab();
        }
        private void AddPathToLab()
        {
            LabArray.ArrayReader(AppDomain.CurrentDomain.BaseDirectory + "Lab.txt");
            int count = 0;
            int[] masX = new int[listX.Count];
            if (listX.Count == 0)
                MessageBox.Show("Выставленные препятствия блокируют путь", "Ошибка");
            int[,] masXY = new int[listX.Count, listY.Count];
            foreach (int x in listX)
            {
                masX[count] = x;
                count++;
            }
            count = 0;
            int[] masY = new int[listY.Count];
            foreach (int y in listY)
            {
                masY[count] = y;
                count++;
            }
            int i = 0;
            while (i < masX.Length)
            {
                LabArray.Lab[masX[i], masY[i]] = 4;
                i++;
            }
        }
    }
    public class WaveAlgorythm : ShortestWay
    {
        public int[,] Map { get; private set; }
        public int MapWidth { get; private set; }
        public int MapHeigth { get; private set; }
        private List<int> listX { get; set; }
        private List<int> listY { get; set; }
        public WaveAlgorythm()
        {
            Map = new int[MapHeigth, MapWidth];
            Map = LabArray.Lab;
            MapWidth = LabArray.Width;
            MapHeigth = LabArray.Height;
            listX = new List<int>();
            listY = new List<int>();
    }
        
        public void FindPath(int startHeight, int startWidth, int endHeight, int endWidth)
        {

            LabArray.ArrayWidthFinder();
            LabArray.ArrayHeightFinder();


            int[,] Map = new int[LabArray.Height, LabArray.Width];
            Map = LabArray.Lab;

            int y, x = 0;
            int step = 0;

            for (x = 0; x < LabArray.Height; x++)
                for (y = 0; y < LabArray.Width; y++)
                {
                    if (Map[x, y] == 1)
                        Map[x, y] = -2;
                    else
                    if (Map[x, y] == 2)
                        Map[x, y] = 0;
                    else Map[x, y] = -1;
                }

            Map[endHeight, endWidth] = -1;

            while (Map[endHeight, endWidth] == -1 && step < LabArray.Width * LabArray.Height)
            {
                for (x = 0; x < LabArray.Height - 1; x++)
                {
                    for (y = 0; y < LabArray.Width - 1; y++)
                    {
                        if (Map[x, y] == step)
                        {
                            if (x != 0 && y != 0)
                            {
                                if (Map[x - 1, y] != -2 && Map[x - 1, y] == -1)
                                    Map[x - 1, y] = step + 1;

                                if (Map[x, y - 1] != -2 && Map[x, y - 1] == -1)
                                    Map[x, y - 1] = step + 1;
                            }

                            if (Map[x + 1, y] != -2 && Map[x + 1, y] == -1)
                                Map[x + 1, y] = step + 1;

                            if (Map[x, y + 1] != -2 && Map[x, y + 1] == -1)
                                Map[x, y + 1] = step + 1;
                        }
                    }
                }
                step++;
            }

            y = 0;
            x = 0;
            if (Map[endHeight, endWidth] != -1)
            {
                x = endHeight;
                y = endWidth;
                listX.Add(endHeight);
                listY.Add(endWidth);
                bool temp = true;
                while (x != startHeight || y != startWidth || temp == true)
                {
                    if (Map[x - 1, y] == Map[x, y] - 1)
                    {
                        x -= 1;
                        listX.Add(x);
                        listY.Add(y);
                    }
                    if (y != startWidth)
                        if (Map[x, y - 1] == Map[x, y] - 1)
                        {
                            y -= 1;
                            listX.Add(x);
                            listY.Add(y);
                        }
                    if (Map[x + 1, y] == Map[x, y] - 1)
                    {
                        x += 1;
                        listX.Add(x);
                        listY.Add(y);
                    }
                    if (y != endWidth)
                        if (Map[x, y + 1] == Map[x, y] - 1)
                        {
                            y += 1;
                            listX.Add(x);
                            listY.Add(y);
                        }
                    if (listY.Min() == 0)
                        temp = false;
                    if (listX.Count * listY.Count > LabArray.Height * LabArray.Width * 2)
                        temp = false;
                }
            }
            AddPathToLab();
        }
        private void AddPathToLab()
        {
            LabArray.ArrayReader(AppDomain.CurrentDomain.BaseDirectory + "Lab.txt");
            int count = 0;
            int[] masX = new int[listX.Count];
            if (listX.Count == 0)
                MessageBox.Show("Выставленные препятствия блокируют путь", "Ошибка");
            int[,] masXY = new int[listX.Count, listY.Count];
            foreach (int x in listX)
            {
                masX[count] = x;
                count++;
            }
            count = 0;
            int[] masY = new int[listY.Count];
            foreach (int y in listY)
            {
                masY[count] = y;
                count++;
            }
            int i = 0;
            while (i < masX.Length)
            {
                LabArray.Lab[masX[i], masY[i]] = 4;
                i++;
            }
        }
    }
}
