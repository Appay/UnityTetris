using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    
    public static int SinirGenislik = 10;
    public static int SinirUzunluk = 20;

    public static Transform[,] grid = new Transform[SinirGenislik, SinirUzunluk];


    void Start()
    {
        TetrominoSpawn();
    }


    public bool SinirDisi(Tetromino tetromino)
    {
        for ( int x = 0; x < SinirGenislik; ++x)
        {
            foreach (Transform mino in tetromino.transform)
            {
                Vector2 pos = Round(mino.position);
                if (pos.y > SinirUzunluk-1)
                {
                    return true;
                }
            }
        }

        return false;
    }




    public bool ZeminFull(int y)
    {
        for(int x=0; x < SinirGenislik; ++x)
        {
            if (grid[x, y] == null)
            {
                return false;
            }
        }
        return true;
    }

    public void DeleteMino(int y)
    {
        for (int x = 0; x<SinirGenislik; ++x)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }

    public void MoveDown ( int y)
    {
        for (int x = 0; x<SinirGenislik; ++x)
        {
            if (grid[x, y] != null)
            {
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;
                grid[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    public void MoveAllDown(int y )
    {
        for (int i = y; i< SinirUzunluk; ++i)
        {
            MoveDown(i);
        }
    }

    public void DeleteRow()
    {
        for (int y=0; y<SinirUzunluk; ++y)
        {
            if (ZeminFull(y))
            {
                DeleteMino(y);
                MoveAllDown(y + 1);
                --y;

            }
        }
    }


    public void UpdateSinir(Tetromino tetromino)
    {
        for(int y=0; y<SinirUzunluk; ++y)
        {
            for(int x = 0; x< SinirGenislik; ++x)
            {
                if (grid[x, y] != null)
                {
                    if(grid[x,y].parent == tetromino.transform)
                    {
                        grid[x, y] = null;

                    }
                }
            }
        }
        foreach (Transform mino in tetromino.transform)
        {
            Vector2 pos = Round(mino.position);
            if (pos.y < SinirUzunluk)
            {
                grid[(int)pos.x, (int)pos.y] = mino;
            }
        }


    }

    public Transform GetTransformAtGridPosition (Vector2 pos)
    {
        if(pos.y > SinirUzunluk - 1)
        {
            return null;
        }
        else
        {
            return grid[(int)pos.x, (int)pos.y];
        }
    }

    public void TetrominoSpawn()
    {
      GameObject nextTetromino = (GameObject)Instantiate(Resources.Load(RandomTetromino(), typeof(GameObject)), new Vector2(5.0f, 20.0f), Quaternion.identity); 
    }

    public bool  SinirKontrol ( Vector2 pos)
    {
        return ((int)pos.x >= 0 && (int)pos.x < SinirGenislik && (int)pos.y >= 0);
    }
    
    public Vector2 Round ( Vector2 pos)
    {
        return new Vector2(Mathf.Round(pos.x), Mathf.Round(pos.y));
    }



    string RandomTetromino()
    {
        int rastgeleTetromino = Random.Range(1, 8);
        string RandomTetrominoAdi = "Prefabs/Tetromino_T";
        switch (rastgeleTetromino)
        {
            case 1:
                RandomTetrominoAdi = "Prefabs/Tetromino_T";
                break;
            case 2:
                RandomTetrominoAdi = "Prefabs/Tetromino_Long";
                break;
            case 3:
                RandomTetrominoAdi = "Prefabs/Tetromino_Square";
                break;
            case 4:
                RandomTetrominoAdi = "Prefabs/Tetromino_J";
                break;
            case 5:
                RandomTetrominoAdi = "Prefabs/Tetromino_L";
                break;
            case 6:
                RandomTetrominoAdi = "Prefabs/Tetromino_S";
                break;
            case 7:
                RandomTetrominoAdi = "Prefabs/Tetromino_Z";
                break;
        }
        return RandomTetrominoAdi;
    }

    public void GameOver()
    {
        Application.LoadLevel("GameOver");
    }
}


