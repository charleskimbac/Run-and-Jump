using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float jumpForce;
    private Vector3 gravityMod = new Vector3(0,-42, 0);
    public bool onGround = true;
    public bool gameOver = false;
    private Animator playerAnim;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public AudioClip jumpSound, crashSound;
    private AudioSource playerAudio;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity = gravityMod;
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && onGround && !gameOver){
            onGround = false;
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 1f);
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Ground") {
            onGround = true;
            if (!gameOver)
                dirtParticle.Play();
        }
        else if (collision.gameObject.tag == "Obstacle") {
            gameOver = true;
            Debug.Log("game over");
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            dirtParticle.Stop();
            explosionParticle.Play();
            playerAudio.PlayOneShot(crashSound, 1f);
            playerRb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    public bool isGameOver() {
        return gameOver;
    }
}
