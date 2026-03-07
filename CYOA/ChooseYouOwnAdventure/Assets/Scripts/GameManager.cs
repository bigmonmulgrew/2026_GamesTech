using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [System.Serializable]
    private class StoryPage
    {   // This is a data only class, so public fields are fine here.
        public string Text;
        public string Decision1Text;
        public string Decision2Text;
        public int NextPage1;
        public int NextPage2;
    }

    // Create array of story pagges with a default set of 10 pages
    // The ; is not missing here it is hidden at the end of the region, the array is being initialized with a default set of 10 pages.
    [SerializeField] StoryPage[] storyPages = new StoryPage[10]
    {
        new StoryPage
        {
            Text = "Page 1\n" +
            "You wake up in a simple prison cell with no idea how you got there. " +
            "There's only a bed and an empty bucket. " +
            "You have nothing on you other than a lock pick. " +
            "You also notice the cell has an open window",
            Decision1Text = "Pick the lock.",
            Decision2Text = "Climb out the window.",
            NextPage1 = 2,
            NextPage2 = 3
        },
        new StoryPage
        {
            Text = "Page 2\n" +
            "You manage to pick the lock and quietly move down the corridor. Ahead you see set of stairs leading upwards to the roof and a corridor. Down the corridor is a guard but he has his back to you and you can hear snoring",
            Decision1Text = "Sneak past the guard",
            Decision2Text = "Go up to the roof",
            NextPage1 = 4,
            NextPage2 = 5
        },
        new StoryPage
        {
            Text = "Page 3\n" +
            "Pulling the bed up and balancing the bucket you manage to grab the edge of the window and pull yourself up. There is a 50 foot drop but the stones are rough, maybe you can climb",
            Decision1Text = "Go back to cell",
            Decision2Text = "Climb down",
            NextPage1 = 1,
            NextPage2 = 6
        },
        new StoryPage
        {
            Text = "Page 4\n" +
            "You sneak towards the guard but he stirs in his seat. Quickly you dart into a side room. It appears to be a bathroom. " +
            "Taking a moment to listen you hear guards talking and approaching outside. Maybe you should try to run. " +
            "You also hear what appears to be running water, lifting wooden board on the latrine it appears the fort you find yourslef in is waterside",
            Decision1Text = "Surprise the guard and run",
            Decision2Text = "Climb down through the latrine",
            NextPage1 = 7,
            NextPage2 = 8
        },
        new StoryPage
        {
            Text = "Page 5\n" +
            "You tiptoe up to the roof. Looking around the ground is only around 75 feet away with rough stones. It will be difficult but you could climb down. Looking around there are also some storage crates up here. Maybe they have something useful",
            Decision1Text = "Climb down",
            Decision2Text = "Search the crates",
            NextPage1 = 6,
            NextPage2 = 9
        },
        new StoryPage
        {
            Text = "Page 6\n" +
            "You slip and fall to your death. \n\n" +
            "Game Over!",
            Decision1Text = "Try again",
            Decision2Text = "Also Try again",
            NextPage1 = 1,
            NextPage2 = 1
        },
        new StoryPage
        {
            Text = "Page 7\n" +
            "Paniced by the approaching voices you try to make a run past the guard. " +
            "He awakens too late but calls out. Other guards burst through the doors ahead. You are trapped\n\n" +
            "Game over",
            Decision1Text = "Better luck next time",
            Decision2Text = "Try again?",
            NextPage1 = 1,
            NextPage2 = 1
        },
        new StoryPage
        {
            Text = "Page 8\n" +
            "Lifting the boards you hand down from the edge of the seat. Trying to grip the stone wall you slip. Falling with a splash you land in the water dazed but unharmed.",
            Decision1Text = "Swim away as fast as you can",
            Decision2Text = "Quietly swim aeway",
            NextPage1 = 10,
            NextPage2 = 10
        },
        new StoryPage
        {
            Text = "Page 9\n" +
            "Searching the crates you find a rope long enough to make it to the ground. The coast looks clear.",
            Decision1Text = "Tie rope to crates",
            Decision2Text = "Tie rope torch mounting",
            NextPage1 = 10,
            NextPage2 = 10
        },
        new StoryPage
        {
            Text = "Page 10\n" +
            "Finally with a little distance you are not far from the tree line and can make a run to freedom.\n\n" +
            "You escaped successfully!",
            Decision1Text = "Start again..",
            Decision2Text = "Start again, with enthusiasm!",
            NextPage1 = 1,
            NextPage2 = 1
        }
    };
    
    [SerializeField] InputAction inputAction1;
    [SerializeField] InputAction inputAction2;

    // TMP text reference to display the story text and decisions
    [SerializeField] TextMeshProUGUI storyText;
    [SerializeField] TextMeshProUGUI decision1Text;
    [SerializeField] TextMeshProUGUI decision2Text;

    int currentPage;

    private void Start()
    {
        LoadPage(1);
    }
    private void LoadPage(int pageNumber)
    {
        // Set the current page to the page number passed in
        currentPage = pageNumber;

        // Get the page, - 1 because the array is
        // 0 indexed but our page numbers start at 1
        StoryPage page = storyPages[currentPage - 1];

        // Apply the page text and decision text to the UI
        storyText.text = $"{page.Text}";
        decision1Text.text = $"{page.Decision1Text}";
        decision2Text.text = $"{page.Decision2Text}";
    }

    void OnEnable()
    {
        inputAction1.Enable();
        inputAction2.Enable();
    }
    void OnDisable()
    {
        inputAction1.Disable();
        inputAction2.Disable();
    }
    private void Update()
    {
        if (inputAction1.WasPressedThisFrame())
        {
            Decision1Made();
        }
        if (inputAction2.WasPressedThisFrame())
        {
            Decision2Made();
        }
    }

    private void Decision1Made()
    {
        StoryPage page = storyPages[currentPage - 1];

        LoadPage(page.NextPage1);
    }

    private void Decision2Made()
    {
        StoryPage page = storyPages[currentPage - 1];

        LoadPage(page.NextPage2);
    }

    public void Button1Click()
    {
        Decision1Made();
    }
    public void Button2Click()
    {
        Decision2Made();
    }
}
