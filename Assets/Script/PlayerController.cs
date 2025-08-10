using UnityEngine;
using DG.Tweening;
using System;

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
    //sesler için
    [SerializeField] AudioClip bonusSound, CoinSound, DeathSound, MagnetCoinSound, ShieldSound, WinSound;
    [SerializeField] AudioSource PlayerSound;//oyuncu Sesleri için
    //VfX ler için
    [SerializeField] GameObject coinCollectedVFX, deathVFX, healthDeclineVFX, magnetVFX, wallBreakVFX,ShieldVFX;

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


    //þimdi boollarý tanýmlayalým.

    public bool is2XActive, isShieldActive, isMagnetActive;
    //saðlýk ekleyelim.
    [SerializeField] int Health;


    float beforeSpeed;


    bool isMove;
    //zýplama için deneme1
    bool isJumping = false;
    float jumpTimer = 0f;
    [SerializeField] float jumpHeight = 2f;
    [SerializeField] float jumpDuration = 0.5f;
    float initialY; // zýplama öncesi y konumu


    void Start()
    {
        // transform.position = new Vector3(0, 0, 1);
        isMiddle = true;
        // myAnim.SetBool("Run", true);

        initialY = transform.position.y;
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
        if (isDead) return;


        if (is2XActive)
        {
            floatScore += Time.deltaTime;
        }

        floatScore += Time.deltaTime;
        if (floatScore > 1)
        {
            score += 1;
            floatScore = 0;
        }
        if (passedTime > 10)
        {
            speed += 0.3f;
            passedTime = 0;
        }

        #region karakter sýnýrlama yöntemleri
        transform.Translate(Vector3.forward * speed * Time.deltaTime);


        if (Input.GetKeyDown(KeyCode.A) && transform.position.x > -0.5f && !isMove)
        {
            // transform.Translate(new Vector3(-shift, 0, 0));
            transform.DOMoveX(transform.position.x - shift, 0.5f).SetEase(Ease.Linear).OnComplete(isMoveToFalse);

            isMove = true;
        }
        else if (Input.GetKeyDown(KeyCode.D) && transform.position.x < 0.5f && !isMove)
        {
            // transform.Translate(new Vector3(shift, 0, 0));
            transform.DOMoveX(transform.position.x + shift, 0.5f).SetEase(Ease.Linear).OnComplete(isMoveToFalse);
            isMove = true;
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
          }*/
        /* if (Input.GetKeyDown(KeyCode.Space)) 
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

        // yeni zýplama için 
        // Zýplama baþlat
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            isJumping = true;
            jumpTimer = 0f;
            // myAnim.SetBool("Jump", true);
            myAnim.SetTrigger("JumpTrigger");
        }

        // Zýplama iþlemi (yukarý-aþaðý hareket)
        if (isJumping)
        {
            jumpTimer += Time.deltaTime;
            float percent = jumpTimer / jumpDuration;
            float yOffset = Mathf.Sin(percent * Mathf.PI) * jumpHeight;

            Vector3 pos = transform.position;
            pos.y = initialY + yOffset;
            transform.position = pos;

            if (jumpTimer >= jumpDuration)
            {
                // Zýplama tamamlandý
                isJumping = false;
                transform.position = new Vector3(transform.position.x, initialY, transform.position.z);
                myAnim.SetBool("Jump", false);
            }
        }




    }


    void isMoveToFalse()
    {

        isMove = false;

    }
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("çarpýþtýk");

        if (other.gameObject.CompareTag("obstacle"))
        {
            int damage = other.gameObject.GetComponent<Obstacle>().damage;

            if (isShieldActive)
            {
                Destroy(other.gameObject);
                isShieldActive = false;
                GameObject vfx = Instantiate(wallBreakVFX, other.transform.position, Quaternion.identity);
                Destroy(vfx, 1f);

            }
            else
            {
                CheckHealth(damage, other.gameObject);
            }

        }
    }

    private void CheckHealth(int damage, GameObject other)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Debug.Log("çarpýþtýk");
            myAnim.SetBool("Death", true);
            PlayerSound.PlayOneShot(DeathSound);
            // GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.Euler(-90,0,0));
            GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity);
            Destroy(vfx, 1f);

            isDead = true;
        }
        else
        {
            Destroy(other.gameObject);
            GameObject vfx = Instantiate(wallBreakVFX, other.transform.position, Quaternion.identity);
            Destroy(vfx, 1f);

            GameObject healthvfx = Instantiate(healthDeclineVFX, transform.position, Quaternion.identity,this.transform);
            Destroy(healthvfx, 1f);



        }
      /*  if (score>300)
        {
            PlayerSound.PlayOneShot(WinSound);
        }
      */
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("collectable"))
        {
            Collectables collectables = other.GetComponent<Collectables>();
            switch (collectables.collectablesEnum)
            {
                case CollectablesEnum.Coin:
                    AddScrore(collectables.toBeAddedScore);
                    break;

                case CollectablesEnum.Shield:
                    ActivateShield();
                    break;
                case CollectablesEnum.Health:
                    AddHealth(collectables.TobeAddedHealth);
                    break;
                case CollectablesEnum.Score2X:
                    ActivateBonus();
                    break;
                case CollectablesEnum.SpeedUp:
                    AddSpeed(collectables.toBeAddedSpeed);
                    break;

                case CollectablesEnum.Magnet:
                    ActivateMagnet();

                    break;

            }

            Destroy(other.gameObject);// coinlerin yok olmasý
        }
    }

    private void AddSpeed(int toBeAddedSpeed)
    {
        beforeSpeed = speed;
        speed += toBeAddedSpeed;
        Invoke("BackToOrijinalSpeed", 5f);
    }


    void BackToOrijinalSpeed()
    {

        speed = beforeSpeed;
    }
    void AddScrore(int TobeAddedScore)
    {
        if (isMagnetActive)
        {
            PlayerSound.clip = MagnetCoinSound;//magnetse
           
                PlayerSound.Play();
            
        }
        else
        {
            PlayerSound.clip = CoinSound;//coinse
            
                PlayerSound.Play();
            
        }


        // GameObject vfx= Instantiate(coinCollectedVFX, transform.position, Quaternion.identity);
        //  GameObject vfx= Instantiate(coinCollectedVFX, transform.position, Quaternion.identity,this.transform);
        GameObject vfx = Instantiate(coinCollectedVFX, transform.position+ new Vector3(0,1,0), Quaternion.identity, this.transform);
        Destroy(vfx, 1f);

        if (is2XActive)
        {
            TobeAddedScore *= 2;
        }
        score += TobeAddedScore;
    }

    void ActivateShield()
    {
        isShieldActive = true;
        PlayerSound.PlayOneShot(ShieldSound);
        GameObject vfx = Instantiate(ShieldVFX, transform.position , Quaternion.identity, this.transform);
        Destroy(vfx, 5f);
        Invoke("DeactivateShield", 5f);
    }

    void DeactivateShield()
    {
        isShieldActive = false;
    }

    void AddHealth(int ToBeAddedHealth)
    {
        Health += ToBeAddedHealth;
        if (Health <= 0)
        {
            Debug.Log("çarpýþtýk");
            myAnim.SetBool("Death", true);
            isDead = true;
        }
    }


    void ActivateBonus()
    {
        is2XActive = true;
        AudioSource.PlayClipAtPoint(bonusSound, transform.position);
        Invoke("DeActivateBonus", 5f);
     
     
    }

    void DeActivateBonus()
    {
        is2XActive = false;
    }

    void ActivateMagnet()
    {
        isMagnetActive = true;
        GameObject vfx = Instantiate(magnetVFX, this.transform.position+ new Vector3(0,1,0), Quaternion.identity, this.transform);
        Destroy(vfx, 5f);

        Invoke("DeActivateMagnet", 5f);
    }

    void DeActivateMagnet()
    {
        isMagnetActive = false;
    }
}
