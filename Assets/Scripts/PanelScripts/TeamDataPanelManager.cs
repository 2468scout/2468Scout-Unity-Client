using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class TeamDataPanelManager : MonoBehaviour
    {
        UIManager manager;
        public Team team = null;
        public SimpleTeam simpleTeam;
        public List<Texture2D> picturesArray = null;
        public GameObject robotImage;
        public GameObject fieldHeatImage;
        RectTransform fieldHeatImageRectTransform;
        public int pictureIndex, heatSelectionIndex;
        public int prevPictureIndex = -1;
        bool bIsPullingTeam;
        List<Point> pointsList;
        List<bool> successesList;
        List<float> accuraciesList;
        List<GameObject> xList;
        Sprite redXSprite, greenXSprite, blueXSprite;

        Button backButton, leftButton, rightButton, heatLeftButton, heatRightButton;
        

        Text teamNameNumberText, leftButtonText, rightButtonText, gamesScoutedText, winPercentageText, backButtonText;
        
            //General Texts
            Text generalText, highGoalsPerGameText, lowGoalsPerGameText, gearsPerGameText, pointContText, rpPerGameText, generalSixText, generalSevenText, generalEightText, generalNineText, generalTenText;

            //Robot Texts
            Text robotText, highGoalAccuracyText, speedText, weightText, bestSuitedRoleText, defenseCapText, antidefenseCapText, cakeSkillText, compOfDrivingText, robotNineText, robotTenText;

            //Autonomous Texts 
            Text autonomousText, autoCapText, startPosText, autonomousThreeText, autonomousFourText, autonomousFiveText;

            //End game Texts 
            Text endGameText, climbPercText, touchpadPercText, endGameThreeText, endGameFourText, endGameFiveText;

            //Likelihoods Texts
            Text likelihoodText, penaltyLikeText, breakdownLikeText, stuckLikeText, likelihoodsFourText, likelihoodsFiveText;

            //Heatmap Texts
            Text heatSelectionText;

        



        // Use this for initialization
        void Start()
        {
            manager = GetComponentInParent<UIManager>();
            Debug.Log("Starting TeamDataPanel: SimpleTeam: " + JsonUtility.ToJson(simpleTeam));
            SetData();
            Debug.Log(GetComponentsInChildren<Button>()[0].ToString());
            Debug.Log(GetComponentsInChildren<Button>()[1].ToString());
            
            backButton = GameObject.Find("ToolbarPanel").GetComponentsInChildren<Button>()[0];
            backButton.onClick.AddListener(() => { manager.BackPanel(); });
            leftButton = GetComponentsInChildren<Button>()[0];
            rightButton = GetComponentsInChildren<Button>()[1];
            heatLeftButton = GameObject.Find("heatMapPanel").GetComponentsInChildren<Button>()[0];
            heatRightButton = GameObject.Find("heatMapPanel").GetComponentsInChildren<Button>()[1];
            heatSelectionText = GameObject.Find("heatMapPanel").GetComponentsInChildren<Text>()[3];
            picturesArray = new List<Texture2D>();
            fieldHeatImage = GameObject.Find("fieldHeatImage");
            fieldHeatImageRectTransform = fieldHeatImage.GetComponent<RectTransform>();
            Debug.Log("" + ((fieldHeatImageRectTransform.anchorMax.x - fieldHeatImageRectTransform.anchorMin.x)));
            fieldHeatImageRectTransform.anchorMin = new Vector2(0.25f, 0.4f-((fieldHeatImageRectTransform.anchorMax.x - fieldHeatImageRectTransform.anchorMin.x) * 768f / 2048f));
            fieldHeatImageRectTransform.anchorMax = new Vector2(0.75f, 0.4f+((fieldHeatImageRectTransform.anchorMax.x - fieldHeatImageRectTransform.anchorMin.x) * 768f / 2048f));
            redXSprite = Resources.Load("xred.png") as Sprite;
            greenXSprite = Resources.Load("xgreen.png") as Sprite;
            blueXSprite = Resources.Load("xblue.png") as Sprite;
        }

        // Update is called once per frame
        void Update()
        {
            if(team.sTeamName != simpleTeam.sTeamName && simpleTeam != null && (!bIsPullingTeam))
            {
                Debug.Log("Starting pull coroutine");
                StartCoroutine(PullTeamFromServer());
            }
   
            if(picturesArray != null && picturesArray.Count != 0 &&  (pictureIndex != prevPictureIndex))
            {
                robotImage.GetComponent<Image>().overrideSprite = Sprite.Create((Texture2D)picturesArray[pictureIndex], new Rect(0f, 0f, (picturesArray[pictureIndex]).width, ((Texture2D)picturesArray[pictureIndex]).height), new Vector2(0.5f, 0.5f));
                prevPictureIndex = pictureIndex;
            }
        }
        public void NavigateLeft()
        {
            if (pictureIndex > 0)
            {
                pictureIndex--;
            }
        }
        public void NavigateRight()
        {
            if (pictureIndex < picturesArray.Count-1)
            {
                pictureIndex++;
            }
        }
        public void NavigateHeatLeft()
        {
            if (heatSelectionIndex > 0)
            {
                heatSelectionIndex--;
            }
            switch (heatSelectionIndex)
            {
                case 0: heatSelectionText.text = "Gears Heatmap"; break;
                case 1: heatSelectionText.text = "Low Goal Heatmap"; break;
                case 2: heatSelectionText.text = "High Goal Heatmap"; break;
                case 3: heatSelectionText.text = "Climb Heatmap"; break;
                case 4: heatSelectionText.text = "Hoppers Heatmap"; break;
            }
            updatePoints();
        }
        public void NavigateHeatRight()
        {
            if (heatSelectionIndex < 4)
            {
                heatSelectionIndex++;
            }
            switch (heatSelectionIndex)
            {
                case 0: heatSelectionText.text = "Gears Heatmap"; break;
                case 1: heatSelectionText.text = "Low Goal Heatmap"; break;
                case 2: heatSelectionText.text = "High Goal Heatmap"; break;
                case 3: heatSelectionText.text = "Climb Heatmap"; break;
                case 4: heatSelectionText.text = "Hoppers Heatmap"; break;
            }
            updatePoints();
        }
        public void updatePoints()
        {
            xList.Clear();
            if (heatSelectionIndex == 0)
            {
                pointsList = team.gearMapPointList;
                for (int i = 0; i < pointsList.Count; i++)
                {
                    Point p = pointsList[i];
                    GameObject g = new GameObject();
                    SpriteRenderer renderer = g.AddComponent<SpriteRenderer>();
                    RectTransform rect = g.AddComponent<RectTransform>();
                    g.GetComponent<RectTransform>().anchorMin = new Vector2(p.x, p.y);
                    g.GetComponent<RectTransform>().anchorMax = new Vector2(p.x, p.y);
                    renderer.sprite = blueXSprite;
                    xList.Add(g);
                    g = null; renderer = null; rect = null; p = null;
                }
            }
            if (heatSelectionIndex == 1)
            {
                pointsList = team.lowGoalMapPointList;
                accuraciesList = team.lowGoalMapFloatList;
                for (int i = 0; i < pointsList.Count; i++)
                {
                    Point p = pointsList[i];
                    GameObject g = new GameObject();
                    SpriteRenderer renderer = g.AddComponent<SpriteRenderer>();
                    RectTransform rect = g.AddComponent<RectTransform>();
                    g.GetComponent<RectTransform>().anchorMin = new Vector2(p.x, p.y);
                    g.GetComponent<RectTransform>().anchorMax = new Vector2(p.x, p.y);
                    renderer.sprite = redXSprite;
                    xList.Add(g);
                    g = null; renderer = null; rect = null;
                    GameObject g2 = new GameObject();
                    SpriteRenderer renderer2 = g2.AddComponent<SpriteRenderer>();
                    RectTransform rect2 = g2.AddComponent<RectTransform>();
                    g2.GetComponent<RectTransform>().anchorMin = new Vector2(p.x, p.y);
                    g2.GetComponent<RectTransform>().anchorMax = new Vector2(p.x, p.y);
                    renderer2.sprite = greenXSprite;
                    renderer2.color = new Color(renderer2.color.r, renderer2.color.g, renderer2.color.b, accuraciesList[i]);
                    xList.Add(g2);
                    g2 = null; renderer2 = null; rect2 = null; p = null;
                }
            }
            if (heatSelectionIndex == 2)
            {
                pointsList = team.gearMapPointList;
                for (int i = 0; i < pointsList.Count; i++)
                {
                    Point p = pointsList[i];
                }
            }
            if (heatSelectionIndex == 3)
            {
                pointsList = team.gearMapPointList;
                for (int i = 0; i < pointsList.Count; i++)
                {
                    Point p = pointsList[i];
                }
            }
            if (heatSelectionIndex == 4)
            {
                pointsList = team.gearMapPointList;
                foreach (Point p in pointsList)
                {
                    Point p = pointsList[i];
                }
            }
        }
        IEnumerator PullTeamFromServer()
        {
            WWW pullFromServer = new WWW(manager.sGetTeamURL + simpleTeam.iTeamNumber + "/" + simpleTeam.iTeamNumber + ".json");
            Debug.Log("Pulling Team data from " + manager.sGetTeamURL + simpleTeam.iTeamNumber + "/" + simpleTeam.iTeamNumber + ".json");
            bIsPullingTeam = true;
            yield return pullFromServer;
            team = JsonUtility.FromJson<Team>(pullFromServer.text);
            StartCoroutine(DownloadPictures());
            SetData();
            yield break;
        }
        
        IEnumerator DownloadPictures()
        {
            picturesArray.Clear();
            for (int i = 0; i < team.iNumPictures; i++)
            { 
                WWW PicturesURL = new WWW(manager.sGetTeamURL + "/" + team.iTeamNumber + "/" + team.iTeamNumber + "_" + i + ".jpg");
                Debug.Log("Downloading picture: " + team.iTeamNumber + "_" + i + ".jpg");
                yield return PicturesURL;
                Debug.Log(PicturesURL.bytes.Length);
                Debug.Log("Downloaded picture");
                Texture2D tex = PicturesURL.texture;
                //tex.Resize((int)robotImage.GetComponent<RectTransform>().sizeDelta.x, (int)robotImage.GetComponent<RectTransform>().sizeDelta.y);
                picturesArray.Add(tex);
            }
            yield break;
        }
        void SetData()
        {
            Debug.Log("Setting Data: " + JsonUtility.ToJson(team));
            Text[] textArray = GetComponentsInChildren<Text>();
            //back button functionality
            //backButton = GetComponentInChildren<Button>();
            //backButton.onClick.AddListener(() => { manager.CreatePanelWrapper("analyticsPanel"); });
            int spot = 0;
            teamNameNumberText = textArray[spot]; spot++;
            leftButtonText = textArray[spot]; spot++;
            rightButtonText = textArray[spot]; spot++;
            gamesScoutedText = textArray[spot]; spot++;
            winPercentageText = textArray[spot]; spot++;


            //general------------------------------------------------------------------------------------
            generalText = textArray[spot]; spot++;
            highGoalsPerGameText = textArray[spot]; spot++;
            lowGoalsPerGameText = textArray[spot]; spot++;
            gearsPerGameText = textArray[spot]; spot++;
            pointContText = textArray[spot]; spot++;
            rpPerGameText = textArray[spot]; spot++;
            generalSixText = textArray[spot]; spot++;
            generalSevenText = textArray[spot]; spot++;
            generalEightText = textArray[spot]; spot++;
            generalNineText = textArray[spot]; spot++;
            generalTenText = textArray[spot]; spot++;

            //robot
            robotText = textArray[spot]; spot++;
            highGoalAccuracyText = textArray[spot]; spot++;
            speedText = textArray[spot]; spot++;
            weightText = textArray[spot]; spot++;
            bestSuitedRoleText = textArray[spot]; spot++;
            defenseCapText = textArray[spot]; spot++;
            antidefenseCapText = textArray[spot]; spot++;
            cakeSkillText = textArray[spot]; spot++;
            compOfDrivingText = textArray[spot]; spot++;
            robotNineText = textArray[spot]; spot++;
            robotTenText = textArray[spot]; spot++;

            //Autonomous
            autonomousText = textArray[spot]; spot++;
            autoCapText = textArray[spot]; spot++;
            startPosText = textArray[spot]; spot++;
            autonomousThreeText = textArray[spot]; spot++;
            autonomousFourText = textArray[spot]; spot++;
            autonomousFiveText = textArray[spot]; spot++;

            //End game
            endGameText = textArray[spot]; spot++;
            climbPercText = textArray[spot]; spot++;
            touchpadPercText = textArray[spot]; spot++;
            endGameThreeText = textArray[spot]; spot++;
            endGameFourText = textArray[spot]; spot++;
            endGameFiveText = textArray[spot]; spot++;

            //Likelihood
            likelihoodText = textArray[spot]; spot++;
            penaltyLikeText = textArray[spot]; spot++;
            breakdownLikeText = textArray[spot]; spot++;
            stuckLikeText = textArray[spot]; spot++;
            likelihoodsFourText = textArray[spot]; spot++;
            likelihoodsFiveText = textArray[spot]; spot++;



            //personalizing data to specific team

            teamNameNumberText.text = "" + team.iTeamNumber + " " + team.sTeamName;  //team.teamNameNumberText

            robotImage = GameObject.Find("robotImage");
            //robotImage = team.robotImage;
            robotImage.GetComponent<Image>().preserveAspect = true;

            int gamesScouted = team.iGamesScouted; //team.gamesScouted;
            gamesScoutedText.text = "Games Scouted: " + gamesScouted;

            float winPercentage = team.fWinPercentage; //team.winPercentage;
            winPercentageText.text = "Win Percentage: " + (winPercentage * 100) + "%";


            //GENERAL==========================================================================
            generalText.text = "Averages (Per Match )";

            float highGoals = team.fAvgHighFuelPerMatch; //team.highGoalsPerGame
            highGoalsPerGameText.text = "High Goals: " + highGoals;

            float lowGoals = team.fAvgLowFuelPerMatch; //team.lowGoalsPerGame
            lowGoalsPerGameText.text = "Low Goals: " + lowGoals;


            float gears = team.fAvgGearsPerMatch; //team.gearsPerGame
            gearsPerGameText.text = "Gears: " + gears;

            double pointCont = 50;
            pointContText.text = "Point Contribution: " + pointCont;

            float rpPerGame = team.fAvgRankingPoints;
            rpPerGameText.text = "Ranking Points: " + rpPerGame;

            generalSixText.text = "";
            generalSevenText.text = "";
            generalEightText.text = "";
            generalNineText.text = "";
            generalTenText.text = "";

            //ROBOT==========================================================================
            robotText.text = "Robot Statistics";

            float highGoalAccuracy = team.fHighGoalAccuracy; //team.highGoalAccuracy
            highGoalAccuracyText.text = "High Goal Accuracy: " + highGoalAccuracy + "%";

            int speed = team.iSpeed; //team.speed
            speedText.text = "Speed: " + speed;

            int weight = team.iWeight; //team.weight
            weightText.text = "Weight: " + weight;

            string bestSuitedRole = team.sBestRole; //team.bestSuitedRole
            bestSuitedRoleText.text = "Best Role: " + bestSuitedRole;

            int defenseCap = 2; //team.defenseCap
            defenseCapText.text = "Defense Capability: " + defenseCap;

            int antidefenseCap = 3; //team.antidefenseCap
            antidefenseCapText.text = "Antidefense Capability: " + antidefenseCap;

            int cakeSkill = 0;
            cakeSkillText.text = "Cake ability: " + cakeSkill;

            int competencyOfDriving = 5;
            compOfDrivingText.text = "Competency of Driving: " + competencyOfDriving;

            robotNineText.text = "";
            robotTenText.text = "";

            //AUTONOMOUS==========================================================================
            autonomousText.text = "Autonomous";

            string autoCap = "score";
            autoCapText.text = "Capabilities: " + autoCap;

            int startPos = team.iStartingPosition;
            startPosText.text = "Starting pos: " + startPos;

            autonomousThreeText.text = "";
            autonomousFourText.text = "";
            autonomousFiveText.text = "";

            //END GAME==========================================================================
            endGameText.text = "End Game";

            float climbPerc = team.fClimbAttemptPercent;
            climbPercText.text = "Climb: " + (climbPerc * 100) + "%";

            float touchpadPerc = team.fTouchpadPercent;
            touchpadPercText.text = "TouchPad: " + (touchpadPerc * 100) + "%";

            endGameThreeText.text = "";
            endGameFourText.text = "";
            endGameFiveText.text = "";

            //LIKELIHOOD==========================================================================
            likelihoodText.text = "Likelihoods";

            float penaltyLike = team.fPenaltyLikelihood;
            penaltyLikeText.text = "Penalty Chance: " + (penaltyLike * 100) + "%";

            float breakdownLike = team.fBreakdownLikelihood;
            breakdownLikeText.text = "Breakdown Chance: " + (breakdownLike * 100) + "%";

            float stuckLike = team.fStuckLikelihood;
            stuckLikeText.text = "Stuck: " + (stuckLike * 100) + "%";

            likelihoodsFourText.text = "";
            likelihoodsFiveText.text = "";
        }
    }
}