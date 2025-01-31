using Xunit;
using Tic_Tac_Toe;
using static Tic_Tac_Toe.MiniMax;

// Creating a reference to the project being tested:
// https://learn.microsoft.com/en-us/visualstudio/ide/how-to-add-or-remove-references-by-using-the-reference-manager?view=vs-2022

namespace MiniMax.Tests
{
    public class MiniMax_test1
    {
        private readonly Tic_Tac_Toe.MiniMax _miniMax;

        public MiniMax_test1()
        {
            _miniMax = new Tic_Tac_Toe.MiniMax();
        }

        [Fact]
        public void Test1()
        {
            var miniMax = new Tic_Tac_Toe.MiniMax();
            Move theMove = Tic_Tac_Toe.MiniMax.findBestMove(miniMax.board, 'X', 'O');

            Assert.False((theMove.row == 1), "Bad row");
        }
    }

}

