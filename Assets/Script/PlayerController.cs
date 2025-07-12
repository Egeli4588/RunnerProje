using UnityEngine;

public enum Positions 
{
    onLeft,
    onMiddle,
    onRight

}
public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] Animator myAnim;
    [SerializeField] float speed;
    [SerializeField] float shift = 2;

    [SerializeField] bool isLeft, isMiddle, isRight;

    // Enum tanýmlam

    [SerializeField] Positions positions = Positions.onMiddle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // transform.position = new Vector3(0, 0, 1);
        isMiddle = true;
    }

    // Update is called once per frame
    void Update()
    {
        #region karakter sýnýrlama yöntemleri
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && positions!=Positions.onLeft)

        {
            if (positions==Positions.onMiddle)
            {
                positions = Positions.onLeft;
        
            }
            else if (positions==Positions.onRight)
            {
                positions = Positions.onMiddle;

            }
            transform.Translate(new Vector3(-shift, 0, 0));


        }
        else if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && positions!=Positions.onRight)
        {
            if (positions==Positions.onMiddle)
            {
                positions = Positions.onRight;
            }
            else if (positions==Positions.onLeft)
            {
                positions = Positions.onMiddle;

            }
            transform.Translate(new Vector3(shift, 0, 0));
        }






        /*  if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && isLeft == false)

          {
              if (isMiddle)
              {
                  isLeft = true;
                  isMiddle = false;
              }
              else if (isRight)
              {
                  isMiddle = true;
                  isRight = false;

              }
              transform.Translate(new Vector3(-shift, 0, 0));


          }
          else if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && isRight == false)
          {
              if (isMiddle)
              {
                  isRight = true;
                  isMiddle = false;
              }
              else if (isLeft)
              {
                  isMiddle = true;
                  isLeft = false;

              }
              transform.Translate(new Vector3(shift, 0, 0));
          }

          */


        /*  if (Input.GetKey(KeyCode.A))
          {
              myAnim.SetBool("Run", true);
          }
          else if (Input.GetKeyUp(KeyCode.A))
          {
              myAnim.SetBool("Run", false);
          }
          if (Input.GetKeyDown(KeyCode.Space)) 
          {
              myAnim.SetBool("Jump", true);
          }
          else if (Input.GetKeyUp(KeyCode.Space))
          {
              myAnim.SetBool("Jump", false);
          }
        */
        //hareket 1.yol.


        /* if (Input.GetKeyDown(KeyCode.A)  && transform.position.x>-0.5f)
         {
             transform.Translate(new Vector3(-shift, 0, 0));
         }
         else if (Input.GetKeyDown(KeyCode.D) && transform.position.x<0.5f)
         {
             transform.Translate(new Vector3(shift, 0, 0));
         }*/



        // hareket 2.yol.
        #endregion



    }

    /* private void FixedUpdate()
     {
         rb.MovePosition(transform.position + Vector3.forward * speed * Time.deltaTime);
     }*/
}
