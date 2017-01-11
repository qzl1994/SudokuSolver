using UnityEngine;
using UnityEngine.UI;
using System;

namespace SudokuSolver
{
    [Serializable]
    public class SudokuObject
    {
        public int id;
        public int row;
        public int col;
        public int area;
        public int value;
        public bool constant;
        public InputField inputField;

        public SudokuObject(int _id, int _row, int _col, int _value, GameObject _sudoku)
        {
            this.id = _id;
            this.row = _row;
            this.col = _col;
            this.area = ( _row / 3 ) + ( _col / 3 ) * 3;
            this.value = _value;
            this.constant = false;
            this.inputField = _sudoku.GetComponent<InputField>();
            this.inputField.onValueChanged.AddListener(OnValueChanged);

            //Debug.Log(id + " " + row + " " + " " + col + " " + area);

            if (area % 2 == 0)
            {
                _sudoku.GetComponent<Image>().color = Color.gray;
            }
        }

        public void OnValueChanged(string value)
        {
            //if (this.constant)
            //{
            //    this.inputField.text = this.value.ToString();
            //}
            //else
            //{
            //    int result = 0;
            //    if (( int.TryParse(value, out result) && ( result <= 9 ) ) && ( result > 0 ))
            //    {
            //        this.value = result;
            //    }
            //}

            int result = 0;
            if (( int.TryParse(value, out result) && ( result <= 9 ) ) && ( result > 0 ))
            {
                this.value = result;
            }
        }

        public void ChangeValue()
        {
            this.value = Mathf.Clamp(this.value + 1, 0, 9);
            this.inputField.text = this.value.ToString();
        }

        public void ResetValue()
        {
            this.value = 0;
            this.inputField.text = 0.ToString();
        }
    }
}