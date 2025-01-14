using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 1.0f;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private GameObject _plantPrefab;
    [SerializeField] private int _numSeeds = 5; 
    [SerializeField] private PlantCountUI _plantCountUI;

    private int _numSeedsLeft;
    private int _numSeedsPlanted;

    private void Start ()
    {
        _numSeedsLeft = _numSeeds; // Start with the initial seed count
        _numSeedsPlanted = 0; // No seeds planted initially

        // Update the UI with the initial values
        _plantCountUI.UpdateSeeds(_numSeedsLeft, _numSeedsPlanted);
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Move the player
        Vector3 movement = new Vector3(horizontal, vertical, 0) * _speed * Time.deltaTime;
        _playerTransform.position += movement;

        // Plant a seed when space is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlantSeed();
        }
    }

    public void PlantSeed ()
    {
        // Check if seeds are available
        if (_numSeedsLeft > 0)
        {
            // Creates plant at player's location
            Instantiate(_plantPrefab, _playerTransform.position, Quaternion.identity);

            // Update the seed counts
            _numSeedsLeft--;
            _numSeedsPlanted++;

            // Update the UI
            _plantCountUI.UpdateSeeds(_numSeedsLeft, _numSeedsPlanted);
        }
        else
        {
            Debug.Log("No seeds left to plant!");
        }
    }
}
