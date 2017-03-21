namespace SZ40
{
    using System;
    using System.Text;

    public class Pair<T, K>
    {
        public Pair(T value, K val)
        {
            First = value;
            Second = val;
        }

        public T First { get; set; }

        public K Second { get; set; }
    }

    public class Teletype
    {
        public Teletype()
        {
            this._currentAlphabet = new Pair<int, int>(1, 26);

            this._alphabets = new Pair<int, char>[4, 56];

            #region
            this._alphabets[0, 0] = new Pair<int, char>(70, 'À');
            this._alphabets[0, 1] = new Pair<int, char>(44, 'Á');
            this._alphabets[0, 2] = new Pair<int, char>(68, 'Â');
            this._alphabets[0, 3] = new Pair<int, char>(85, 'Ã');
            this._alphabets[0, 4] = new Pair<int, char>(76, 'Ä');
            this._alphabets[0, 5] = new Pair<int, char>(84, 'Å');
            this._alphabets[0, 6] = new Pair<int, char>(96, '¨');
            this._alphabets[0, 7] = new Pair<int, char>(59, 'Æ');
            this._alphabets[0, 8] = new Pair<int, char>(80, 'Ç');
            this._alphabets[0, 9] = new Pair<int, char>(66, 'È');
            this._alphabets[0, 10] = new Pair<int, char>(81, 'É');
            this._alphabets[0, 11] = new Pair<int, char>(82, 'Ê');
            this._alphabets[0, 12] = new Pair<int, char>(75, 'Ë');
            this._alphabets[0, 13] = new Pair<int, char>(86, 'Ì');
            this._alphabets[0, 14] = new Pair<int, char>(89, 'Í');
            this._alphabets[0, 15] = new Pair<int, char>(74, 'Î');
            this._alphabets[0, 16] = new Pair<int, char>(71, 'Ï');
            this._alphabets[0, 17] = new Pair<int, char>(72, 'Ð');
            this._alphabets[0, 18] = new Pair<int, char>(67, 'Ñ');
            this._alphabets[0, 19] = new Pair<int, char>(78, 'Ò');
            this._alphabets[0, 20] = new Pair<int, char>(69, 'Ó');
            this._alphabets[0, 21] = new Pair<int, char>(65, 'Ô');
            this._alphabets[0, 22] = new Pair<int, char>(91, 'Õ');
            this._alphabets[0, 23] = new Pair<int, char>(87, 'Ö');
            this._alphabets[0, 24] = new Pair<int, char>(93, 'Ú');
            this._alphabets[0, 25] = new Pair<int, char>(83, 'Û');
            this._alphabets[0, 26] = new Pair<int, char>(77, 'Ü');
            this._alphabets[0, 27] = new Pair<int, char>(90, 'ß');

            this._alphabets[0, 28] = new Pair<int, char>(1040, 'À');
            this._alphabets[0, 29] = new Pair<int, char>(1041, 'Á');
            this._alphabets[0, 30] = new Pair<int, char>(1042, 'Â');
            this._alphabets[0, 31] = new Pair<int, char>(1043, 'Ã');
            this._alphabets[0, 32] = new Pair<int, char>(1044, 'Ä');
            this._alphabets[0, 33] = new Pair<int, char>(1045, 'Å');
            this._alphabets[0, 34] = new Pair<int, char>(1025, '¨');
            this._alphabets[0, 35] = new Pair<int, char>(1046, 'Æ');
            this._alphabets[0, 36] = new Pair<int, char>(1047, 'Ç');
            this._alphabets[0, 37] = new Pair<int, char>(1048, 'È');
            this._alphabets[0, 38] = new Pair<int, char>(1049, 'É');
            this._alphabets[0, 39] = new Pair<int, char>(1050, 'Ê');
            this._alphabets[0, 40] = new Pair<int, char>(1051, 'Ë');
            this._alphabets[0, 41] = new Pair<int, char>(1052, 'Ì');
            this._alphabets[0, 42] = new Pair<int, char>(1053, 'Í');
            this._alphabets[0, 43] = new Pair<int, char>(1054, 'Î');
            this._alphabets[0, 44] = new Pair<int, char>(1055, 'Ï');
            this._alphabets[0, 45] = new Pair<int, char>(1056, 'Ð');
            this._alphabets[0, 46] = new Pair<int, char>(1057, 'Ñ');
            this._alphabets[0, 47] = new Pair<int, char>(1058, 'Ò');
            this._alphabets[0, 48] = new Pair<int, char>(1059, 'Ó');
            this._alphabets[0, 49] = new Pair<int, char>(1060, 'Ô');
            this._alphabets[0, 50] = new Pair<int, char>(1061, 'Õ');
            this._alphabets[0, 51] = new Pair<int, char>(1062, 'Ö');
            this._alphabets[0, 52] = new Pair<int, char>(1063, 'Ú');
            this._alphabets[0, 53] = new Pair<int, char>(1064, 'Û');
            this._alphabets[0, 54] = new Pair<int, char>(1065, 'Ü');
            this._alphabets[0, 55] = new Pair<int, char>(1071, 'ß');
            #endregion

            #region
            this._alphabets[1, 0] = new Pair<int, char>(65, 'A');
            this._alphabets[1, 1] = new Pair<int, char>(66, 'B');
            this._alphabets[1, 2] = new Pair<int, char>(67, 'C');
            this._alphabets[1, 3] = new Pair<int, char>(68, 'D');
            this._alphabets[1, 4] = new Pair<int, char>(69, 'E');
            this._alphabets[1, 5] = new Pair<int, char>(70, 'F');
            this._alphabets[1, 6] = new Pair<int, char>(71, 'G');
            this._alphabets[1, 7] = new Pair<int, char>(72, 'H');
            this._alphabets[1, 8] = new Pair<int, char>(73, 'I');
            this._alphabets[1, 9] = new Pair<int, char>(74, 'J');
            this._alphabets[1, 10] = new Pair<int, char>(75, 'K');
            this._alphabets[1, 11] = new Pair<int, char>(76, 'L');
            this._alphabets[1, 12] = new Pair<int, char>(77, 'M');
            this._alphabets[1, 13] = new Pair<int, char>(78, 'N');
            this._alphabets[1, 14] = new Pair<int, char>(79, 'O');
            this._alphabets[1, 15] = new Pair<int, char>(80, 'P');
            this._alphabets[1, 16] = new Pair<int, char>(81, 'Q');
            this._alphabets[1, 17] = new Pair<int, char>(82, 'R');
            this._alphabets[1, 18] = new Pair<int, char>(83, 'S');
            this._alphabets[1, 19] = new Pair<int, char>(84, 'T');
            this._alphabets[1, 20] = new Pair<int, char>(85, 'U');
            this._alphabets[1, 21] = new Pair<int, char>(86, 'V');
            this._alphabets[1, 22] = new Pair<int, char>(87, 'W');
            this._alphabets[1, 23] = new Pair<int, char>(88, 'X');
            this._alphabets[1, 24] = new Pair<int, char>(89, 'Y');
            this._alphabets[1, 25] = new Pair<int, char>(90, 'Z');
            #endregion

            #region
            this._alphabets[2, 0] = new Pair<int, char>(48, '0');
            this._alphabets[2, 1] = new Pair<int, char>(49, '1');
            this._alphabets[2, 2] = new Pair<int, char>(50, '2');
            this._alphabets[2, 3] = new Pair<int, char>(51, '3');
            this._alphabets[2, 4] = new Pair<int, char>(52, '4');
            this._alphabets[2, 5] = new Pair<int, char>(53, '5');
            this._alphabets[2, 6] = new Pair<int, char>(54, '6');
            this._alphabets[2, 7] = new Pair<int, char>(55, '7');
            this._alphabets[2, 8] = new Pair<int, char>(56, '8');
            this._alphabets[2, 9] = new Pair<int, char>(57, '9');
            this._alphabets[2, 10] = new Pair<int, char>(73, 'Ø');
            this._alphabets[2, 11] = new Pair<int, char>(79, 'Ù');
            this._alphabets[2, 12] = new Pair<int, char>(39, 'Ý');
            this._alphabets[2, 13] = new Pair<int, char>(88, '×');
            this._alphabets[2, 14] = new Pair<int, char>(46, 'Þ');
            #endregion

            #region
            this._alphabets[3, 0] = new Pair<int, char>(55, '?'); // 63
            this._alphabets[3, 1] = new Pair<int, char>(45, '-'); // 45
            this._alphabets[3, 2] = new Pair<int, char>(49, '!'); // 33
            this._alphabets[3, 3] = new Pair<int, char>(56, '*'); // 42
            this._alphabets[3, 4] = new Pair<int, char>(44, ','); // 44
            this._alphabets[3, 5] = new Pair<int, char>(54, ':'); // 58
            this._alphabets[3, 6] = new Pair<int, char>(57, '('); // 40
            this._alphabets[3, 7] = new Pair<int, char>(48, ')'); // 41
            this._alphabets[3, 8] = new Pair<int, char>(43, '+'); // 43
            this._alphabets[3, 9] = new Pair<int, char>(46, '.'); // 46
            this._alphabets[3, 10] = new Pair<int, char>(61, '='); // 61
            this._alphabets[3, 11] = new Pair<int, char>(47, '/'); // 47
            this._alphabets[3, 12] = new Pair<int, char>(63, '?'); // 47
            this._alphabets[3, 13] = new Pair<int, char>(42, '*'); // 42
            this._alphabets[3, 14] = new Pair<int, char>(58, ':'); // 58
            #endregion

            this._text = new StringBuilder();
        }

        public void Input(char ch)
        {
           
            if (this.CheckAlphabet(ch))
            {
                return;
            }

            ch = Char.ToUpper(ch);

            int pos = this.FindIndex(ch);

            if (pos != -1)
            {
                this._text.AppendFormat("{0}", this._alphabets[this._currentAlphabet.First, pos].Second);
            }
        }

        public int FindIndex(char ch)
        {
            for (int i = 0; i < this._currentAlphabet.Second; ++i)
            {
                if (this._alphabets[this._currentAlphabet.First, i].First == ch)
                    return i;
            }

            return -1;
        }

        public string GetText()
        {
            if (this._text == null)
                return String.Empty;

            return this._text.ToString();
        }

        private bool CheckAlphabet(char ch)
        {
            if (ch == '#') // #
            {
                this._text.Append(ch);
                this._currentAlphabet.First = 0;
                this._currentAlphabet.Second = 56;
                return true;
            }

            if (ch == '$') // $
            {
                this._text.Append(ch);
                this._currentAlphabet.First = 1;
                this._currentAlphabet.Second = 26;
                return true;
            }

            if (ch == '@') // @
            {
                this._text.Append(ch);
                this._currentAlphabet.First = 2;
                this._currentAlphabet.Second = 15;
                return true;
            }

            if (ch == '&') // &
            {
                this._text.Append(ch);
                this._currentAlphabet.First = 3;
                this._currentAlphabet.Second = 15;
                return true;
            }

            return false;
        }

        private StringBuilder _text;

        private Pair<int, char>[,] _alphabets;

        private Pair<int, int> _currentAlphabet;
    }
}