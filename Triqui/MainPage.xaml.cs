using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Triqui
{
    public partial class MainPage : ContentPage
    {
        private string currentPlayer = "X";
        private string[,] tablero = new string[3, 3];//declaro una matriz 3x3 que es para representar el tablero
        private string player1Name;
        private string player2Name;
        public MainPage()
        {
            InitializeComponent();
            ResetBoard();
        }
        private void StartGame_Clicked(object sender, EventArgs e)
        {
            player1Name = player1Entry.Text;
            player2Name = player2Entry.Text;

            if (string.IsNullOrEmpty(player1Name) || string.IsNullOrEmpty(player2Name))
            {
                DisplayAlert("Error", "Por favor, ingrese los nombres de ambos jugadores.", "OK");
                return;
            }

            ResetBoard();
            gameGrid.IsVisible = true;
        }
        private void ResetBoard()
        {
            currentPlayer = "X";
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    tablero[i, j] = string.Empty;
                }
            }
            btn00.Text = "";
            btn01.Text = "";
            btn02.Text = "";
            btn10.Text = "";
            btn11.Text = "";
            btn12.Text = "";
            btn20.Text = "";
            btn21.Text = "";
            btn22.Text = "";
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            int row = Grid.GetRow(button);
            int col = Grid.GetColumn(button);

            if (tablero[row, col] == string.Empty)
            {
                tablero[row, col] = currentPlayer;
                button.Text = currentPlayer;
                if (CheckWin())
                {
                    string winnerName = currentPlayer == "X" ? player1Name : player2Name;
                    DisplayAlert("Game Over", $"{winnerName} Ganó!", "OK");
                    ResetBoard();
                }
                else if (IsBoardFull())
                {
                    DisplayAlert("Game Over", "Es un empate!", "OK");
                    ResetBoard();
                }
                else
                {
                    currentPlayer = (currentPlayer == "X") ? "O" : "X";
                }
            }
        }

        private bool CheckWin()
        {
            // Verificar filas, columnas y diagonales
            for (int i = 0; i < 3; i++)
            {
                if (tablero[i, 0] == currentPlayer && tablero[i, 1] == currentPlayer && tablero[i, 2] == currentPlayer)
                    return true;
                if (tablero[0, i] == currentPlayer && tablero[1, i] == currentPlayer && tablero[2, i] == currentPlayer)
                    return true;
            }
            if (tablero[0, 0] == currentPlayer && tablero[1, 1] == currentPlayer && tablero[2, 2] == currentPlayer)
                return true;
            if (tablero [0, 2] == currentPlayer && tablero[1, 1] == currentPlayer && tablero[2, 0] == currentPlayer)
                return true;

            return false;
        }

        private bool IsBoardFull()
        {
            foreach (var cell in tablero)
            {
                if (cell == string.Empty)
                    return false;
            }
            return true;
        }
    }

}

