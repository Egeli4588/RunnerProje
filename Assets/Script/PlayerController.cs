using UnityEngine;
using DG.Tweening;

public enum Positions
{
    onLeft,
    onMiddle,
    onRight

}
public class PlayerController : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] Rigidbody rb;
    [SerializeField] public Animator myAnim;
    [Header("Settings")]
    [Tooltip("bu deðiþken oyuncunun hýzýný belirler")]
    [SerializeField] float speed;
    [SerializeField] float shift = 2;

    [SerializeField] bool isLeft, isMiddle, isRight;

   public bool isDead;

    public bool isStart;

    [SerializeField] public int score;

    [SerializeField] public float floatScore;
    [SerializeField] public float passedTime;

    // Enum tanýmlam

    [System.NonSerialized] public Positions positions = Positions.onMiddle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // transform.position = new Vector3(0, 0, 1);
        isMiddle = true;
        // myAnim.SetBool("Run", true);
    }

    // Update is called once per frame
    void Update()
    {
        passedTime += Time.deltaTime;
        moveCharacter();

    }

    /* private void FixedUpdate()
     {
         rb.MovePosition(transform.position + Vector3.forward * speed * Time.deltaTime);
     }*/

    /// <summary>
    /// bu metod karakter hareketi ve sýnýrlandýrma iþlemlerini yapar.
    /// </summary>
    void moveCharacter()
    {

        if (!isStart) return;
         if (isDead)   return;

        floatScore += Time.deltaTime;
        if (floatScore>1)
        {
            score += 1;
            floatScore = 0;
        }
        if (passedTime>10)
        {
            speed += 0.3f;
            passedTime = 0;
        }

        #region karakter sýnýrlama yöntemleri
        transform.Translate(Vector3.forward * speed * Time.deltaTime);


        if (Input.GetKeyDown(KeyCode.A) && transform.position.x > -0.5f)
        {
            // transform.Translate(new Vector3(-shift, 0, 0));
            transform.DOMoveX(transform.position.x - shift, 0.5f).SetEase(Ease.Linear);


        }
        else if (Input.GetKeyDown(KeyCode.D) && transform.position.x < 0.5f)
        {
            // transform.Translate(new Vector3(shift, 0, 0));
            transform.DOMoveX(transform.position.x + shift, 0.5f).SetEase(Ease.Linear); ;
        }


        /* if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && positions != Positions.onLeft)

         {
             if (positions == Positions.onMiddle)
             {
                 positions = Positions.onLeft;

             }
             else if (positions == Positions.onRight)
             {
                 positions = Positions.onMiddle;

             }
             transform.Translate(new Vector3(-shift, 0, 0));


         }
         else if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && positions != Positions.onRight)
         {
             if (positions == Positions.onMiddle)
             {
                 positions = Positions.onRight;
             }
             else if (positions == Positions.onLeft)
             {
                 positions = Positions.onMiddle;

             }
             transform.Translate(new Vector3(shift, 0, 0));
         }

         */




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





        // hareket 2.yol.
        #endregion



    }

    private void OnCollisionEnter(Collision other)
    {
         Debug.Log("çarpýþtýk");

        if (other.gameObject.CompareTag("obstacle"))
        {
            Debug.Log("çarpýþtýk");
            myAnim.SetBool("Death", true);
            isDead = true;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("coin")) 
        {
            score += 10;
            Destroy(other.gameObject);
        }
    }


}
