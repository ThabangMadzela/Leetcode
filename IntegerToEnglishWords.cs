using System;

class Solution
{
    private string[] belowTwenty = { "", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten",
                                      "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
    
    private string[] tens = { "", "", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

    private string[] thousands = { "", "Thousand", "Million", "Billion" };

    public string NumberToWords(int num)
    {
        if (num == 0)
            return "Zero";

        string words = ConvertToWords(num);
        return words.Trim();
    }

    private string ConvertToWords(int num)
    {
        string words = "";

        if (num < 20)
        {
            words = belowTwenty[num];
        }
        else if (num < 100)
        {
            words = tens[num / 10] + " " + ConvertToWords(num % 10);
        }
        else if (num < 1000)
        {
            words = ConvertToWords(num / 100) + " Hundred " + ConvertToWords(num % 100);
        }
        else
        {
            for (int i = 0; i < thousands.Length; i++)
            {
                if (num >= 1000 && num % 1000 != 0)
                {
                    words = ConvertToWords(num / 1000) + " " + thousands[i] + " " + ConvertToWords(num % 1000);
                }

                num /= 1000;
            }
        }

        return words.Trim();
    }
}

class Program
{
    static void Main(string[] args)
    {
        Solution solution = new Solution();

        int num1 = 123;
        string words1 = solution.NumberToWords(num1);
        Console.WriteLine($"Input: {num1}");
        Console.WriteLine($"Output: \"{words1}\"");

        int num2 = 12345;
        string words2 = solution.NumberToWords(num2);
        Console.WriteLine($"Input: {num2}");
        Console.WriteLine($"Output: \"{words2}\"");
    }
}
