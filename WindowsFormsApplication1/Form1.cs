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

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private List<LevelItemType> LevelItemTypes = new List<LevelItemType>
        {
            new LevelItemType{
                bytes = new byte[] { 0x67, 0x6F, 0x6C, 0x64, 00 },
                type = LevelItemTypeEnum.Gold
            },
            new LevelItemType{
                bytes = new byte[] {0x41, 0x76, 0x6F, 0x69, 0x64, 0x31, 0x00 },
                type = LevelItemTypeEnum.Avoid1
            },
            new LevelItemType{
                bytes = new byte[] {0x53, 0x74, 0x61, 0x72, 0x74, 0x43, 0x68, 0x61, 0x6C, 0x6C, 0x65, 0x6E, 0x67, 0x65, 0x00 },
                type = LevelItemTypeEnum.StartChallenge
            },
            new LevelItemType{
                bytes = new byte[] {0x45, 0x6E, 0x64, 0x43, 0x68, 0x61, 0x6C, 0x6C, 0x65, 0x6E, 0x67, 0x65, 0x00 },
                type = LevelItemTypeEnum.EndChallenge
            },
            new LevelItemType{
                bytes = new byte[] {0x50,0x6C,0x61,0x79,0x53,0x6F,0x75,0x6E,0x64,0x00},
                type = LevelItemTypeEnum.Sound //the next 28 are the sound
            },
            

        };

        private void button1_Click(object sender, EventArgs e)
        {
            var x = openFileDialog1.ShowDialog();
            var filestream = new FileStream(openFileDialog1.FileName, FileMode.Open);

            byte[] bytes = new byte[filestream.Length];

            int numBytesToRead = (int)filestream.Length;
            int numBytesRead = 0;
            while (numBytesToRead > 0)
            {
                // Read may return anything from 0 to numBytesToRead.
                int n = filestream.Read(bytes, numBytesRead, numBytesToRead);

                // Break when the end of the file is reached.
                if (n == 0)
                    break;

                numBytesRead += n;
                numBytesToRead -= n;
            }

            var l = DecodeLevel(bytes);
            var sb = new StringBuilder();
            foreach(var item in l.OrderBy(j => j.location))
            {
                sb.AppendLine(item.outputLine());
            }

            var str = sb.ToString();
            Console.Write(str);

            var y = 3;
        }

        private List<LevelItem> DecodeLevel(byte[] level)
        {
            var output = new List<LevelItem>();
            foreach(var type in LevelItemTypes)
            {
                foreach (var location in ByteArrayRocks.Locate(level, type.bytes))
                {
                    switch (type.type)
                    {
                        case LevelItemTypeEnum.Sound:
                            var sb = new byte[28];
                            Array.Copy(level, location + 32, sb, 0, 28);
                            output.Add(new SoundItem { type = type, location = location, soundbytes = sb });
                            break;
                        default:
                            output.Add(new LevelItem { type = type, location = location });
                            break;
                    }
                }
            }

            return output;
        }
    }
}
