using System.ComponentModel.DataAnnotations;

namespace GuessNumber.StateModels
{
    public class MoveModel
    {
        [Range(100, 999)]
        public int GuessNumber { get; set; }
        [Range(100, 999)]
        public int Number { get; set; }
        public string? Hint { get; set; }

        public DateTime PlayTime { get; set; }


        public string FindHint()
        {
            var guesDigits = GuessNumber.ToString().Select(digit => int.Parse(digit.ToString())).ToList();

            var numberDigits = Number.ToString().Select(digit => int.Parse(digit.ToString())).ToList();

            string tempHint = "";

            int correctPlace = 0, wrongPlace = 0;

            // 1. sayı eşit olma durumu
            if (Number == GuessNumber)
                tempHint = "Correct";
            else if (Number == 0)
            {
                tempHint = "Pass";
            }
            else
            {
                for (int i = 0; i < guesDigits.Count; i++)
                {
                    if (guesDigits[i] == numberDigits[i])
                        correctPlace++;
                    else if (guesDigits.Contains(numberDigits[i]))
                        wrongPlace++;
                }
                if (correctPlace == 0 && wrongPlace == 0)
                    tempHint = "Iska";
                else
                {
                    if (correctPlace > 0)
                        tempHint += $"+{correctPlace} ";
                    if (wrongPlace > 0)
                        tempHint += $"-{wrongPlace}";
                }
            }

            Hint = tempHint;
            return Hint;
        }
    }
}
