// Converted from: The Coding Train https://www.youtube.com/watch?v=0ZONMNUKTfU
// https://github.com/CodingTrain/MarchingSquares/blob/master/MarchingSquares_Cabana/MarchingSquares_Cabana.pde
// 
// unity version: https://unitycoder.com/blog/2020/11/14/marching-squares/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarchingSquares : MonoBehaviour
{
    float[,] field;
    float rez = 1f;
    int cols, rows;
    float increment = 0.1f;
    //float zoff = 0;
    float xoff = 0;
    float yoff = 0;

    int width = 64;
    int height = 64;

    float xpos = 0f;
    float xspeed = 0.01f;

    void Start()
    {
        cols = (int)(1 + width / rez);
        rows = (int)(1 + height / rez);
        field = new float[cols, rows];
    }

    void Update()
    {
        Draw();
    }

    void Draw()
    {
        // init field
        xoff = 0;
        for (int i = 0; i < cols; i++)
        {
            xoff += increment;
            yoff = 0;
            for (int j = 0; j < rows; j++)
            {
                field[i, j] = Mathf.PerlinNoise(xoff + xpos, yoff) * 2 - 1;
                yoff += increment;
            }
        }

        xpos += xspeed;

        // draw field
        for (int i = 0; i < cols - 1; i++)
        {
            for (int j = 0; j < rows - 1; j++)
            {
                float x = i * rez;
                float y = j * rez;

                var a = new Vector2(x + rez * 0.5f, y);
                var b = new Vector2(x + rez, y + rez * 0.5f);
                var c = new Vector2(x + rez * 0.5f, y + rez);
                var d = new Vector2(x, y + rez * 0.5f);

                int sa = Mathf.CeilToInt(field[i, j]);
                int sb = Mathf.CeilToInt(field[i + 1, j]);
                int sc = Mathf.CeilToInt(field[i + 1, j + 1]);
                int sd = Mathf.CeilToInt(field[i, j + 1]);

                int state = GetState(sa, sb, sc, sd);

                switch (state)
                {
                    case 1:
                        Line(c, d);
                        break;
                    case 2:
                        Line(b, c);
                        break;
                    case 3:
                        Line(b, d);
                        break;
                    case 4:
                        Line(a, b);
                        break;
                    case 5:
                        Line(a, d);
                        Line(b, c);
                        break;
                    case 6:
                        Line(a, c);
                        break;
                    case 7:
                        Line(a, d);
                        break;
                    case 8:
                        Line(a, d);
                        break;
                    case 9:
                        Line(a, c);
                        break;
                    case 10:
                        Line(a, b);
                        Line(c, d);
                        break;
                    case 11:
                        Line(a, b);
                        break;
                    case 12:
                        Line(b, d);
                        break;
                    case 13:
                        Line(b, c);
                        break;
                    case 14:
                        Line(c, d);
                        break;
                    default:
                        break;
                }
            }
        }

        // draw borders
        float xx = (cols - 1) * rez;
        float yy = (rows - 1) * rez;
        Line(Vector2.zero, new Vector2(xx, 0), Color.black); // bottom
        Line(Vector2.zero, new Vector2(0, yy), Color.black); // left
        Line(new Vector2(xx, 0), new Vector2(xx, yy), Color.black); // right
        Line(new Vector2(0, yy), new Vector2(xx, yy), Color.black); // top
    }

    void Line(Vector2 a, Vector2 b, Color col)
    {
        Debug.DrawLine(a, b, col);
    }

    void Line(Vector2 a, Vector2 b)
    {
        Debug.DrawLine(a, b, Color.white);
    }

    // convert "binary" to int (0 0 0 0 = 0)
    int GetState(int a, int b, int c, int d)
    {
        return a * 8 + b * 4 + c * 2 + d * 1;
    }

}
