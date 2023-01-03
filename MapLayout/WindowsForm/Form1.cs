namespace WinForms;

public partial class Form1 : Form
{
    //variables
    public static int X = 15; // x size
    public static int Y = 7; // y size
    int ChangedN = 1; // number the player is
    int StartX = 1; // players x start position
    int StartY = 1; // players y start position
    int num = 0; // don't change
    int player; // don't change
    int wallN = 2; // number the wall is
    int[] wall = new int[] {
        5 + (X * 2),
        6 + (X * 2),
        7 + (X * 2),
    }; // places for walls to go -- (X + (_ + Y)
    //map layout
    public static string layout = "";
    public static int total = X * Y;
    public static int[] map = new int[total];

    public static Label l = new Label();
    public static Keys key;
    public Form1()
    {
        InitializeComponent();

        System.Windows.Forms.Timer time = new System.Windows.Forms.Timer();
        time.Enabled = true;
        time.Interval = 5;

        l = new Label();
        int buffer = 20;
        l.Left = buffer;
        l.Top = buffer;
        l.Width = Width - buffer;
        l.Height = Height - buffer;
        l.Font = new Font("monospace", 13, FontStyle.Bold);
        Controls.Add(l);



        #pragma warning disable CS8622 // Rethrow to preserve stack details
        this.Load += new EventHandler(_Start);
        this.KeyDown += new KeyEventHandler(_KeyPress);
        time.Tick += new EventHandler(_Update);
        #pragma warning restore CS8622 // Rethrow to preserve stack details

    }
    public void _Start(object sender, EventArgs e)
    {
        //this is the start meathod
        player = StartX + (StartY * X);
        map[player] = ChangedN;
        foreach (int i in wall)
        {
            map[i] = wallN;
        }

        Numbers();
    }
    public void _Update(object sender, EventArgs e)
    {
        //this is the Update meathod

        //PictureBox[] p;
        //p = Output.PrintToPb(layout);
        //for(int i = 0; i < p.Length; i++) l.Controls.Add(p[i]);
        //l.Text = p.Length.ToString();

        Output.Print(layout);
        try
        {
            for (int i = 0; i < map.Length; i++) map[i] = 0;
            layout = string.Empty;
            var r =  key;
            string input = r.ToString();
            int O = map.Length;

            int a = Array.Find<int>(wall, (arr => arr == (player - 1)));
            int w = Array.Find<int>(wall, (arr => arr == (player - X)));
            int s = Array.Find<int>(wall, (arr => arr == (player + X)));
            int d = Array.Find<int>(wall, (arr => arr == (player + 1)));
            if (input == "A" && (player - 1) >= 0 && (player - 1) != 2 && a == 0) player -= 1;
            if (input == "W" && (player - X) >= 0 && (player - X) != 2 && w == 0) player -= X;
            if (input == "S" && (player + X) < O && (player + X) != 2 && s == 0) player += X;
            if (input == "D" && (player + 1) < O && (player + 1) != 2 && d == 0) player += 1;
        
            map[player] = ChangedN;

            Numbers();

            key = Keys.None;
        }
        catch
        {
            Output.WinBox("Error", "An error occured", true);
        }
    }
    void Numbers()
    {
        foreach (int i in wall)
        {
            map[i] = wallN;
        }
        for (int l = 0; l < Y; l++)
        {
            for (int j = 0; j < X; j++)
            {
                layout += (map[num] + " ");
                num++;
                if (num >= map.Length) num = 0;
            }
            layout += "\n";
        }
    }
    public void _KeyPress(object sender, KeyEventArgs e)
    {
        key = Output.Read(e);
        //Output.WinBox("Key", key.ToString(), false);
    }
}
class Output
{
    public static PictureBox[] PrintToPb(string text)
    {
        //Form1.l.Text = text + "; ";

        int size = 30; // size of square
        int spacing = 5; // spacing between each square
        int left = Form1.l.Left; // starting X position
        int top = Form1.l.Top; // starting Y position
        Color Ncolor = Color.Gray; // color of the Normal squares
        Color Pcolor = Color.Gray; // color of the Player's square
        Color Wcolor = Color.Gray; // color of the Wall squares

        List<PictureBox> p = new List<PictureBox>();
        Size s = new Size(size, size);

        for(int i = 0; i < text.Length; i++)
        {
            PictureBox pb = new PictureBox();
            pb.Left = left;
            pb.Top = top;
            pb.Size = s;

            if(text[i] == '0') pb.BackColor = Ncolor;
            else if(text[i] == '1') pb.BackColor = Pcolor;
            else if(text[i] == '2') pb.BackColor = Wcolor;
            else pb.BackColor = Color.Black;

            if(left <= Form1.X * (size + spacing))
                left = left + size + spacing;
            else
            {
                left = Form1.l.Left;
                if(top <= Form1.Y * (size + spacing))
                    top = top + size + spacing;
            }
            pb.BringToFront();
            p.Add(pb);
            //Form1.l.Text += i + ": " + pb.Left.ToString() + "," + pb.Top.ToString() + "  ";
        }
        return p.ToArray();
    }
    public static void Print(string text)
    {
        Form1.l.Text = text;
    }
    public static void Clear()
    {
        Form1.l.Text = string.Empty;
    }
    public static Keys Read(KeyEventArgs e)
    {
        return e.KeyCode;
    }
    public static void WinBox(string caption, string Text, bool error)
    {
        if(error) MessageBox.Show(Text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        else MessageBox.Show(Text, caption, MessageBoxButtons.OK, MessageBoxIcon.None);
    }
}
