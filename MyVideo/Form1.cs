using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//Khai bao sử dụng các thư viện của AForge
using AForge;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Drawing.Imaging;
using System.IO;

namespace MyVideo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Khai bao bien toan cuc
        private FilterInfoCollection CaptureDevice;
        private VideoCaptureDevice FinalFrame;
        DetectPlateNumber detect = new DetectPlateNumber();

        // Các biến sử dụng cho Auto Caputer
        Bitmap video, video2;
        Graphics g;
        bool OnOff = false;
        int thoigiandemnguoc; //Số lượng ảnh cần chụp của 1 cảnh, mỗi ảnh cách nhau 1 giây
        int scenes = 1;  //Số thứ tự cảnh chụp

        private void Form1_Load(object sender, EventArgs e)
        {
            //Lấy các thiết bị Webcam, máy quay đang gắn vào máy tính làm dữ liệu cho combobox
            textBox1.Text = "1";
            textBox2.Text = "1";
            label3.Text = "";
            CaptureDevice = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo Device in CaptureDevice)
            {
                comboBox1.Items.Add(Device.Name);
            }
            comboBox1.SelectedIndex = 0;
            FinalFrame = new VideoCaptureDevice();
        }

        //Khi nút lệnh Start được bấm
        private void button1_Click(object sender, EventArgs e)
        {
            FinalFrame = new VideoCaptureDevice(CaptureDevice[comboBox1.SelectedIndex].MonikerString);
            FinalFrame.NewFrame += new NewFrameEventHandler(FinalFrame_NewFrame);
            FinalFrame.Start();
        }

        private void FinalFrame_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            pictureBox1.Image = (Bitmap)eventArgs.Frame.Clone();
            video = (Bitmap)eventArgs.Frame.Clone();
            Bitmap video2 = (Bitmap)eventArgs.Frame.Clone();
            if (OnOff == true)
            {
                g = Graphics.FromImage(video2);
                g.DrawString(thoigiandemnguoc.ToString(), new Font("Arial", 40), new SolidBrush(Color.Red), new PointF(2, 2));
                g.Dispose();
                pictureBox2.Image = video2;

            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            FinalFrame.Stop();
        }

        public string tranferDate(int number)
        {
            string x = "0" + number.ToString();
            if (x.Length == 3)
            {
                return x.Substring(1, 2);
            }
            return x;
        }

        private string generateTimeName()
        {
            DateTime date = DateTime.Now;
            string hour = tranferDate(date.Hour);
            string minute = tranferDate(date.Minute);
            string second = tranferDate(date.Second);
            string day = tranferDate(date.Day);
            string month = tranferDate(date.Month);
            string year = date.Year.ToString();
            string dateTostring = hour + minute + second + day + month + year;
            return dateTostring;
        }


        private void timer1_Tick(object sender, EventArgs e)
        {

            thoigiandemnguoc -= 1;
            string temp = generateTimeName();
            if (thoigiandemnguoc == 0)
            {
                timer1.Enabled = false;
                OnOff = false;
                /*MessageBox.Show("Đã lưu đủ số ảnh");*/

                textBox2.Text = scenes.ToString();
                // FinalFrame.Stop();
            }
            pictureBox1.Image = video;

            var CurrentDirectory = Directory.GetCurrentDirectory();
            string savePath = CurrentDirectory.Substring(0, CurrentDirectory.Length - 18) + @"\Pictures\" + temp + ".jpg";
            // string savePath = @"E:\HUMG\Nghien cuu khoa hoc sinh vien\MyVideoAutoCaptureSnapshot\MyVideoAutoCaptureSnapshot\Pictures\" + temp + ".jpg";
            pictureBox2.Image.Save(savePath, ImageFormat.Jpeg);
            string plateNumber = PlateActions.getPlateNumber(savePath);
            label3.Text = plateNumber;
            // label3.Text = savePath;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1 != null)
            {
                timer1.Enabled = true;
                OnOff = true;
                thoigiandemnguoc = int.Parse(textBox1.Text);
            }

        }

    }
}
