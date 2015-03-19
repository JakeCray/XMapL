using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace XMapL
{
    #region Form Class
    public partial class Form1 : Form
    {

        String _path;

        public Form1()
        {
            InitializeComponent();
        }

        List<node> nodeList = new List<node>();

        private void Form1_Load(object sender, EventArgs e)
        {

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            _path = ofd.FileName;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            


            /////////////////////////////////////////////////////////////////////////

            XmlTextReader reader = new XmlTextReader(_path);
            richTextBox1.Text = "";

            string nameTest = "";
            int _pIndex = 0;

            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element: // The node is an element.
                        richTextBox1.AppendText("<" + reader.Name);
                        nameTest = reader.Name;
                        while (reader.MoveToNextAttribute())
                        { // Read the attributes.
                            richTextBox1.AppendText(" " + reader.Name + "='" + reader.Value + "'");
                            if (nameTest == "mNode" && reader.Name == "content")
                            {
                                MotherNode motherNode = new MotherNode(reader.Value, ConsoleColor.Red);
                                Console.WriteLine(motherNode.getContent());
                            }
                            else if(nameTest == "node" && reader.Name == "content")
                            {
                                nodeList.Add(new node(reader.Value, ConsoleColor.Red, nodeList.Count(), nodeList.Count()));
                            }
                        }
                        richTextBox1.AppendText(">\n");
                        _pIndex++;
                        break;
                    case XmlNodeType.Text: //Display the text in each element.
                        richTextBox1.AppendText(reader.Value + "\n");
                        break;
                    case XmlNodeType.EndElement: //Display the end of the element.
                        richTextBox1.AppendText("</" + reader.Name);
                        richTextBox1.AppendText(">\n");
                        break;
                }
            }

            /////////////////////////////////////////////////////////////////////////

//            foreach (MotherNode test in nodeList)
//            {
//                richTextBox1.AppendText("\n" + test.getContent());
//            }

        }
    }
    #endregion

    #region Node Class
    /// <summary>
    /// This is the main class of a node we will create child classes of this class.
    /// </summary>
    public class node
    {
        /// <summary>
        /// This class takes in two parameters content and color.
        /// </summary>
        String _content;
        ConsoleColor _color;
        int _index;
        int _pIndex;

        /// <summary>
        /// This class takes in two parameters content and color.
        /// </summary>
        public node(String content, ConsoleColor color, int index, int pIndex)
        {
            _content = content;
            _color = color;
            _index = index;
            _pIndex = pIndex;
            Console.WriteLine(_content + _pIndex + ":" + _index);
        }

        /// <summary>
        /// This class takes in two parameters content and color.
        /// </summary>
        public node(String content, ConsoleColor color, int index)
        {
            _content = content;
            _color = color;
            _index = index;
        }

        /// <summary>
        /// This will return the value of the content of the node.
        /// </summary>
        /// <returns> The content.</returns>
        public String getContent()
        {
            return _content;
        }
    }
#endregion

    #region Mother Node

    public class MotherNode
    {
        /// <summary>
        /// This class takes in two parameters content and color.
        /// </summary>
        String _content;
        ConsoleColor _color;
        int _index = 0;


        node test;

        /// <summary>
        /// This class takes in two parameters content and color.
        /// </summary>
        public MotherNode(String content, ConsoleColor color)
        {
            test = new node(content, color, _index);
        }

        /// <summary>
        /// This will return the value of the content of the node.
        /// </summary>
        /// <returns> The content.</returns>
        public String getContent()
        {
            return test.getContent();
        }
    }
#endregion
}
