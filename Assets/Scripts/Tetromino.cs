using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetromino : MonoBehaviour
{

    float Düsme = 0;
    public float DüsmeHizi = 3;

    public bool DonussIzni = true;
    public bool DonusLimit = false;


    void Start()
    {
        
    }

    
    void Update()
    {
        KullanıcıGirisi();
    }


    void KullanıcıGirisi()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow)) // sağ ok tuşuna basılınca x 1 yönünde ilerlet
        {
            transform.position += new Vector3(1, 0, 0);

            if (PozisyonKontrol())
            {
                FindObjectOfType<Game>().UpdateSinir (this);
            }
            else
            {
                transform.position += new Vector3(-1, 0, 0); // sağa doğru giderken sınırı aşarsa adımı geri almak
            }
        }

        else if (Input.GetKeyDown(KeyCode.LeftArrow)) // sol oka basınca x -1 yönünde ilerlet
        {
            transform.position += new Vector3(-1, 0, 0);

            if (PozisyonKontrol())
            {
                FindObjectOfType<Game>().UpdateSinir(this);
            }
            else
            {
                transform.position += new Vector3(1, 0, 0); 
            }
        }

        else if (Input.GetKeyDown(KeyCode.UpArrow)) // üst oka basınca bloğu 90 derece döndür
        {
            if (DonussIzni)
            {
                if (DonusLimit)
                {
                    if (transform.rotation.eulerAngles.z >= 90)
                    {
                        transform.Rotate(0, 0, -90);
                    }
                    else
                    {
                        transform.Rotate(0, 0, 90);
                    }
                }
                else
                {
                    transform.Rotate(0, 0, 90);

                    if (PozisyonKontrol())
                    {
                        FindObjectOfType<Game>().UpdateSinir(this);
                    }
                    else
                    {
                        if (DonusLimit)
                        {

                            if (transform.rotation.eulerAngles.z >= 90)
                            {
                                transform.Rotate(0, 0, -90);
                            }
                            else
                            {
                                transform.Rotate(0, 0, 90);
                            }
                        }
                        else
                        {
                            transform.Rotate(0, 0, -90);
                        }
                    }
                }
            }
        }

        else if(Input.GetKeyDown(KeyCode.DownArrow) || Time.time - Düsme >=DüsmeHizi) // aşağı oka basınca y -1 yönünde ilerlet
        {
            transform.position += new Vector3(0, -1, 0);

            if (PozisyonKontrol())
            {
                FindObjectOfType<Game>().UpdateSinir(this);
            }
            else
            {
                transform.position += new Vector3(0, 1, 0);

                FindObjectOfType<Game>().DeleteRow();

                if (FindObjectOfType<Game>().SinirDisi(this))
                {
                    FindObjectOfType<Game>().GameOver();
                }


                enabled = false;

                FindObjectOfType<Game>().TetrominoSpawn();
            }

            Düsme = Time.time;       
        }
    }

     bool PozisyonKontrol() // sınırları aşıp aşmadığını anlamak için olan kodlar
     {
        foreach(Transform mino in transform)
        {
            Vector2 pos = FindObjectOfType<Game>().Round(mino.position);
            
            if (FindObjectOfType<Game>().SinirKontrol(pos) == false)
            {
                return false;
            }
            if(FindObjectOfType<Game>().GetTransformAtGridPosition(pos) !=null && FindObjectOfType<Game>().GetTransformAtGridPosition(pos).parent != transform)
            {
                return false;
            }
        }
        return true;
     }
 
}
