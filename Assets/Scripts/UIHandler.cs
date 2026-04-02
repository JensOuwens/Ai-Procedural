using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DungeonUI : MonoBehaviour
{
    [SerializeField] private DungeonConfig config;
    [SerializeField] private GenerationHandler generator;

    [Header("General")]
    public TMP_InputField seedInput;

    [Header("Grid")]
    public TMP_InputField minGridX;
    public TMP_InputField minGridY;
    public TMP_InputField maxGridX;
    public TMP_InputField maxGridY;

    [Header("Random Walk")]
    public TMP_InputField stepsInput;
    public Slider biasSlider; // only slider

    [Header("Rooms")]
    public TMP_InputField minRoomsInput;
    public TMP_InputField maxRoomsInput;

    public TMP_InputField minRoomX;
    public TMP_InputField minRoomY;
    public TMP_InputField maxRoomX;
    public TMP_InputField maxRoomY;

    public TMP_InputField spawnChanceInput;
    public TMP_InputField attemptsInput;

    [Header("Content")]
    public TMP_InputField treasureRoomInput;
    public TMP_InputField hiddenRoomInput;
    public TMP_InputField enemyCorridorInput;
    public TMP_InputField treasureCorridorInput;

    void Start()
    {
        LoadToUI();
        BindUI();
    }

    void LoadToUI()
    {
        seedInput.text = config.seed.ToString();

        minGridX.text = config.minGridSize.x.ToString();
        minGridY.text = config.minGridSize.y.ToString();
        maxGridX.text = config.maxGridSize.x.ToString();
        maxGridY.text = config.maxGridSize.y.ToString();

        stepsInput.text = config.numberOfSteps.ToString();
        biasSlider.value = config.unvisitedBias;

        minRoomsInput.text = config.minRooms.ToString();
        maxRoomsInput.text = config.maxRooms.ToString();

        minRoomX.text = config.minRoomSize.x.ToString();
        minRoomY.text = config.minRoomSize.y.ToString();
        maxRoomX.text = config.maxRoomSize.x.ToString();
        maxRoomY.text = config.maxRoomSize.y.ToString();

        spawnChanceInput.text = config.roomSpawnChance.ToString();
        attemptsInput.text = config.roomMaxAttempts.ToString();

        treasureRoomInput.text = config.treasureRoomChance.ToString();
        hiddenRoomInput.text = config.hiddenRoomChance.ToString();

        enemyCorridorInput.text = config.enemyCorridorSpawnChance.ToString();
        treasureCorridorInput.text = config.treasureCorridorSpawnChance.ToString();
    }

    void BindUI()
    {
        seedInput.onEndEdit.AddListener(v => TrySetInt(v, val => config.seed = val));

        minGridX.onEndEdit.AddListener(v => SetVector2Int(ref config.minGridSize, v, true));
        minGridY.onEndEdit.AddListener(v => SetVector2Int(ref config.minGridSize, v, false));
        maxGridX.onEndEdit.AddListener(v => SetVector2Int(ref config.maxGridSize, v, true));
        maxGridY.onEndEdit.AddListener(v => SetVector2Int(ref config.maxGridSize, v, false));

        stepsInput.onEndEdit.AddListener(v => TrySetInt(v, val => config.numberOfSteps = val));
        biasSlider.onValueChanged.AddListener(v => config.unvisitedBias = v);

        minRoomsInput.onEndEdit.AddListener(v => TrySetInt(v, val => config.minRooms = val));
        maxRoomsInput.onEndEdit.AddListener(v => TrySetInt(v, val => config.maxRooms = val));

        minRoomX.onEndEdit.AddListener(v => SetVector2Int(ref config.minRoomSize, v, true));
        minRoomY.onEndEdit.AddListener(v => SetVector2Int(ref config.minRoomSize, v, false));
        maxRoomX.onEndEdit.AddListener(v => SetVector2Int(ref config.maxRoomSize, v, true));
        maxRoomY.onEndEdit.AddListener(v => SetVector2Int(ref config.maxRoomSize, v, false));

        spawnChanceInput.onEndEdit.AddListener(v => TrySetInt(v, val => config.roomSpawnChance = val));
        attemptsInput.onEndEdit.AddListener(v => TrySetInt(v, val => config.roomMaxAttempts = val));

        treasureRoomInput.onEndEdit.AddListener(v => TrySetInt(v, val => config.treasureRoomChance = val));
        hiddenRoomInput.onEndEdit.AddListener(v => TrySetInt(v, val => config.hiddenRoomChance = val));

        enemyCorridorInput.onEndEdit.AddListener(v => TrySetInt(v, val => config.enemyCorridorSpawnChance = val));
        treasureCorridorInput.onEndEdit.AddListener(v => TrySetInt(v, val => config.treasureCorridorSpawnChance = val));
    }

    void TrySetInt(string value, System.Action<int> setter)
    {
        if (int.TryParse(value, out int result))
            setter(result);
    }

    void SetVector2Int(ref Vector2Int vec, string value, bool isX)
    {
        if (!int.TryParse(value, out int val)) return;

        if (isX) vec.x = val;
        else vec.y = val;
    }

    // Buttons

    public void OnGeneratePressed()
    {
        generator.Generate();
    }

    public void OnRandomSeedPressed()
    {
        generator.RandomizeSeed();
        seedInput.text = config.seed.ToString();
        generator.Generate();
    }
}