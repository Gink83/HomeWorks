using System.Resources;

namespace TreeViewDZ
{
    public partial class Form1 : Form
    {
        public ImageList imagelist = new();

        public Form1()
        {
            InitializeComponent();

            ResourceManager resourceManager = new(typeof(Form1));
            var fileIcon = (Image)resourceManager.GetObject("fileIcon");
            var folderIcon = (Image)resourceManager.GetObject("folderIcon");

            imagelist.Images.Add(fileIcon);
            imagelist.Images.Add(folderIcon);
            treeView.ImageList = imagelist;
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            string rootPath = @"C:\1\";

            DirectoryInfo rootDirectory = new DirectoryInfo(rootPath);

            TreeNode rootNode = new TreeNode(rootDirectory.Name);
            rootNode.Tag = rootDirectory.FullName;
            treeView.Nodes.Add(rootNode);

            LoadDirectory(rootDirectory, rootNode);
        }

        private void LoadDirectory(DirectoryInfo directory, TreeNode parentNode)
        {
            try
            {
                // �������� �� ������ ������������� � ��������� �� � TreeView
                foreach (DirectoryInfo subDirectory in directory.GetDirectories())
                {
                    // ����� ���� ��� �������������
                    TreeNode directoryNode = new TreeNode(subDirectory.Name)
                    {
                        Tag = subDirectory.FullName,
                        ImageIndex = 0,
                        SelectedImageIndex = 0
                    };

                    // ���������� ��������� ������������� � ����� ��� ������� �������������
                    LoadDirectory(subDirectory, directoryNode);

                    // ��������� ���� ������������� � ������������� ����
                    parentNode.Nodes.Add(directoryNode);
                }

                // �������� �� ������� ����� � ������� ���������� � ��������� ��� � TreeView
                foreach (FileInfo file in directory.GetFiles())
                {
                    // ����� ���� ��� �����
                    TreeNode fileNode = new TreeNode(file.Name)
                    {
                        Tag = file.FullName,
                        ImageIndex = 1,
                        SelectedImageIndex = 1
                    };

                    parentNode.Nodes.Add(fileNode);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occured: " + ex.Message);
            }
        }
    }
}