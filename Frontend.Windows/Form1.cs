using Backend;
using System.Drawing;

namespace Frontend.Windows

{
    public partial class Form1 : Form
    {
        private TextBox display;
        private int baseDisplayWidth = 350;

        private List<Button> rightButtons = new List<Button>();

        private int baseRightButtonWidth = 80;
        private Button deleteButton = null!;

        public Form1()
        {
            InitializeComponent();

            display = new TextBox();

            display.Location = new Point(20, 20);
            display.Size = new Size(baseDisplayWidth, 40);

            display.Font = new Font("Arial", 18);
            display.ReadOnly = true;
            display.TextAlign = HorizontalAlignment.Right;
            display.BackColor = Color.Honeydew;

            Controls.Add(display);

            AddNumberButton("7", 20, 80);
            AddNumberButton("8", 110, 80);
            AddNumberButton("9", 200, 80);
            AddNumberButton("4", 20, 140);
            AddNumberButton("5", 110, 140);
            AddNumberButton("6", 200, 140);
            AddNumberButton("1", 20, 200);
            AddNumberButton("2", 110, 200);
            AddNumberButton("3", 200, 200);
            AddNumberButton("0", 20, 260);
            AddDecimalButton(200, 260);
            AddOperatorButton("+", 290, 80);
            AddOperatorButton("-", 290, 140);
            AddOperatorButton("*", 290, 200);
            AddOperatorButton("/", 290, 260);
            AddOperatorButton("^", 20, 320);
            AddOperatorButton("(", 110, 320);
            AddOperatorButton(")", 200, 320);
            AddClearButton(290, 320);
            AddEqualsButton(20, 380);
            AddDeleteButton(290, 380);
        }

        private void AdjustDisplayWidth() // metodo para ajustar el ancho del display cuando la expresion crece
        {
            using Graphics graphics = display.CreateGraphics();

            SizeF textSize = graphics.MeasureString(display.Text, display.Font);

            int requiredWidth = (int)textSize.Width + 30;

            if (requiredWidth < baseDisplayWidth)
            {
                requiredWidth = baseDisplayWidth;
            }
            
            display.Width = requiredWidth;
            Width = display.Left + display.Width + 40;

            AdjustButtonsPosition();
        }

        private void AdjustButtonsPosition() // metodo para mover el tamaño de bonotes el calculos grandes
        {
            int extraWidth = display.Width - baseDisplayWidth;
            int rightButtonWidth = baseRightButtonWidth + extraWidth;
            int rightX = display.Right;

            foreach (Button button in rightButtons)
            {
                 button.Width = rightButtonWidth;
                button.Left = rightX - button.Width;
            }

            deleteButton.Left = rightX - deleteButton.Width;
        }

        private void AddNumberButton(string number, int x, int y) // funcion para agregar botones de numeros
        {
            Button button = new Button();

            button.Text = number;
            button.Location = new Point(x, y);

            if (number == "0")
            {
                button.Size = new Size(170, 50);
            }
            else 
            {
                button.Size = new Size(80, 50);
            }

            button.Click += (sender, e) =>
            {
                display.Text += number;
                AdjustDisplayWidth();
            };

            Controls.Add(button);
        }

        private void AddDecimalButton(int x, int y) // funcion para agregar boton de punto decimal con restriccion de un solo punto decimal
        {
            Button button = new Button();

            button.Text = ".";
            button.Location = new Point(x, y);
            button.Size = new Size(80, 50);

            button.Click += (sender, e) =>
            {
                string[] parts = display.Text.Split('+', '-', '*', '/', '^', '(', ')');

                string currentNumber = parts[^1];

                if (!currentNumber.Contains("."))
                {
                    display.Text += ".";
                    AdjustDisplayWidth();
                }
            };

            Controls.Add(button);
        }

        private void AddOperatorButton(string op, int x, int y) // funcion para agregar botones de operadores
        {
            Button button = new Button();

            button.Text = op;
            button.Location = new Point(x, y);
            button.Size = new Size(80, 50);
            button.BackColor = Color.Coral;

            button.Click += (sender, e) =>
            {
                display.Text += op;
                AdjustDisplayWidth();
            };

            Controls.Add(button);

            if (x == 290)
            {
                rightButtons.Add(button);
            }
        }

        private void AddClearButton(int x, int y) // funcion para agregar boton de limpiar
        {
            Button button = new Button();

            button.Text = "C";
            button.Location = new Point(x, y);
            button.Size = new Size(80, 50);

            button.Click += (sender, e) =>
            {
                display.Text = "";
                AdjustDisplayWidth();
            };

            Controls.Add(button);

            rightButtons.Add(button);
        }

        private void AddDeleteButton(int x, int y)
        {
            Button button = new Button();

            button.Text = "⌫";
            button.Location = new Point(x, y);
            button.Size = new Size(80, 50);

            button.Click += (sender, e) =>
            {
                if (display.Text.Length > 0)
                {
                    display.Text = display.Text.Substring(0, display.Text.Length - 1);
                    AdjustDisplayWidth();
                }
            };

            Controls.Add(button);

            deleteButton = button;
        }

        private void AddEqualsButton(int x, int y) // Solucion "=" conectada a backend
        {
            Button button = new Button();

            button.Text = "=";
            button.Location = new Point(x, y);
            button.Size = new Size(260, 50);

            button.Click += (sender, e) =>
            {
                try
                {
                    string expression = display.Text;
                    double result = ExpressionEvaluator.Evalute(expression);
                    display.Text = $"{expression}={result}";
                    AdjustDisplayWidth();
                }
                catch 
                {
                    display.Text = "Error";
                    AdjustDisplayWidth();
                }
            };

            Controls.Add(button);
        }

    }
}
