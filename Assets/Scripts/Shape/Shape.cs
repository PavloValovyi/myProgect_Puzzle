using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{

    public GameObject squareShapeImage;

    [HideInInspector]
    public ShapeData CurrentShapeData;
    private List<GameObject> _currentShape = new List<GameObject>();

    void Start()
    {
        RequestNewShape(CurrentShapeData); 
    }

    public void RequestNewShape(ShapeData shapeData)
    {
        CreateShape(shapeData);
    }
    public void CreateShape(ShapeData shapeData)
    {
        CurrentShapeData = shapeData;
        var totalSquareNumber = GetNumberOfSquares(shapeData);

        while (_currentShape.Count <= totalSquareNumber)
        {
            _currentShape.Add(Instantiate(squareShapeImage, transform) as GameObject);

        }
        foreach (var square in _currentShape)
        {
            square.gameObject.transform.position = Vector3.zero;
            square.gameObject.SetActive(false);
            var squareRect = squareShapeImage.GetComponent<RectTransform>();
            var moveDistance = new Vector2(squareRect.rect.width * squareRect.localScale.x,
                squareRect.rect.height * squareRect.localScale.y);
            int currentIndexInList = 0;
            // set positin to form final shape
            for (var row = 0; row < shapeData.rows; row++)
            {
                for (var column = 0; column < shapeData.columns; column++)
                {
                    if (shapeData.board[row].column[column])
                    {
                        _currentShape[currentIndexInList].SetActive(true);
                        _currentShape[currentIndexInList].GetComponent<RectTransform>().localPosition =
                            new Vector2(GetXPositionForShapeSquare(shapeData, column, moveDistance),
                            GetYPositionForShapeSquare(shapeData, row, moveDistance));

                        currentIndexInList++;
                    }
                }
            }
        }
    }
    //private void GetYPositionForShapeSquare(ShapeData shapeData, int row, Vector2 moveDistance)
    //{
    //    float shiftOnY = 0f;

    //    if (shapeData.rows > 1)//Vertical position calculation
    //    {
    //        if (shapeData.rows % 2 != 0)
    //        {
    //            var middleSquareIndex = (shapeData.rows - 1) / 2;
    //            var multiplier = (shapeData.rows - 1) / 2;
    //            if (row < middleSquareIndex) // move it on the minus
    //            {
    //                shiftOnY = moveDistance.y * 1;
    //                shiftOnY *= multiplier;
    //            }
    //            else if (row > middleSquareIndex) // move it on plus
    //            {
    //                shiftOnY = moveDistance.y * -1;
    //                shiftOnY *= multiplier;

    //            }
    //            else
    //            {
    //                var middleSquareIndex2 = (shapeData.rows == 2) ? 1 : (shapeData.rows / 2);
    //                var middleSquareIndex1 = (shapeData.rows == 2) ? 0 : shapeData.rows / -2;
    //                var multiplier = shapeData.rows / 2;
    //                if (row == middleSquareIndex1 || row == middleSquareIndex2)
    //                {
    //                    if (row == middleSquareIndex2)
    //                        shiftOnY = (moveDistance.y / 2) * -1;
    //                    if (row == middleSquareIndex1)
    //                        shiftOnY = moveDistance.y / 2;

    //                }
    //                if (row < middleSquareIndex1 && row < middleSquareIndex2) // move it on minus
    //                {
    //                    shiftOnY = moveDistance.y * 1;
    //                    shiftOnY *= multiplier;
    //                }
    //                else if (row > middleSquareIndex1 && row > middleSquareIndex2) // move it on plus
    //                {
    //                    shiftOnY = moveDistance.y * -1;
    //                    shiftOnY *= multiplier;
    //                }
    //            }
    //        }
    //        return shiftOnY;
    //    }
    //}

    //private float GetXPositionForShapeSquare(ShapeData shapeData, int column, Vector2 moveDistance)
    //{
    //    {
    //        float shiftOnX = 0f;

    //        if (shapeData.columns > 1) //Vertical position calculation
    //        {
    //            if (shapeData.columns % 2 != 0)
    //            {
    //                var middleSquareIndex = (shapeData.columns - 1) / 2;
    //                var multiplier = (shapeData.columns - 1) / 2;
    //                if (column < middleSquareIndex) // move it on the negative
    //                {
    //                    shiftOnX = moveDistance.x * -1;
    //                    shiftOnX *= multiplier;
    //                }
    //                else if (column > middleSquareIndex) // move it on plus
    //                {
    //                    shiftOnX = moveDistance.x * 1;
    //                    shiftOnX *= multiplier;
    //                }
    //            }
    //            else
    //            {
    //                var middleSquareIndex2 = (shapeData.columns == 2) ? 1 : (shapeData.columns / 2);
    //                var middleSquareIndex1 = (shapeData.columns == 2) ? 0 : shapeData.columns / -1;
    //                var multiplier = shapeData.columns / 2;
    //                if (column == middleSquareIndex1 || column == middleSquareIndex2)
    //                {
    //                    if (column == middleSquareIndex2)
    //                        shiftOnX = moveDistance.x / 2;
    //                    if (column == middleSquareIndex1)
    //                        shiftOnX = (moveDistance.x / 2) * -1;
    //                }
    //                if (column < middleSquareIndex1 && column < middleSquareIndex2) // move it on negative
    //                {
    //                    shiftOnX = moveDistance.x * -1;
    //                    shiftOnX *= multiplier;
    //                }
    //                else if (column > middleSquareIndex1 && column > middleSquareIndex2) // move it on plus
    //                {
    //                    shiftOnX = moveDistance.x * 1;
    //                    shiftOnX *= multiplier;
    //                }
    //            }
    //        }
    //        return shiftOnX;
    //    }
    private float GetXPositionForShapeSquare(ShapeData shapeData, int column, Vector2 moveDistance)
    {
        float shiftOnX = 0f;
        if (shapeData.columns > 1)
        {
            float startXPos;
            if (shapeData.columns % 2 != 0)
                startXPos = (shapeData.columns / 2) * moveDistance.x * -1;
            else
                startXPos = ((shapeData.columns / 2) - 1) * moveDistance.x * -1 - moveDistance.x / 2;
            shiftOnX = startXPos + column * moveDistance.x;

        }
        return shiftOnX;
    }

    private float GetYPositionForShapeSquare(ShapeData shapeData, int row, Vector2 moveDistance)
    {
        float shiftOnY = 0f;
        if (shapeData.rows > 1)
        {
            float startYPos;
            if (shapeData.rows % 2 != 0)
                startYPos = (shapeData.rows / 2) * moveDistance.y;
            else
                startYPos = ((shapeData.rows / 2) - 1) * moveDistance.y + moveDistance.y / 2;
            shiftOnY = startYPos - row * moveDistance.y;
        }
        return shiftOnY;
    }
    private int GetNumberOfSquares(ShapeData shapeData)
        {
            int number = 0;

            foreach (var rowData in shapeData.board)
            {
                foreach (var active in rowData.column)
                {
                    if (active) 
                    number++;

                }

            }
            return number;
        }
}

