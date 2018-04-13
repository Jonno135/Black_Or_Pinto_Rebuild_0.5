using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;



public class GameManager : MonoBehaviour {
	int[] guess = {0,0,0};
	int[] solution = {0,0,0};
	public static GameManager control;
	public float money;
	public int currentStreak;
	public int lastStreak;
	public int topStreak;

	public float updateTime;
	public bool minimumUpdateTime;
	public float sixtySecondsCounter;
	public float currentTime;
	public float initialTime;
	public int currentSPM;

	public float moneySnapshot;
	public float tenSecondCounter;

	public GameObject black;
	public GameObject pinto;

	public Text moneyText;
	public Text currentStreakText;
	public Text topStreakText;
	public Text lastStreakText;
	public Text oddsText;
	public Text spmText;
	public Text WinLose;




	// Use this for initialization
	void Start () {
		hideBeans ();
		rollBeans ();
		Load ();
		//JON - ON START AND UPDATE CALL SAVE DATA FUNCTION IF EXISTS
//		
	}
	// Update is called once per frame
	void Update () {
		updateAllStats ();
	}
	void FixedUpdate(){
		Save ();
	}


	// On opening the game, saves data between scenes
//	void Awake () {
//		if (control == null) {
//			DontDestroyOnLoad (gameObject);
//			control = this;
//		} else if (control != this) {
//			Destroy (gameObject);
//		}
//	}

//	void OnGUI(){
//		//GUI.Label (new Rect(), "Money: " + money);
//		if (GUI.Button (new Rect(50, 660, 300, 100), "Save")) {
//			GameManager.control.Save ();
//		}
//
//		if (GUI.Button  (new Rect(50, 800, 300, 100), "Load")) {
//			GameManager.control.Load ();
//		}
//	}


	public void setGuessHands(int leftOrRight){
		guess [1] = leftOrRight;
	}

	public void setGuessBeans(int blackOrPinto){
		guess [2] = blackOrPinto;
		//print ("guess hands:" + guess[1]);
		//print ("guess beans:" + guess[2]);
		checkGuess ();
		rollBeans ();
	}

	public bool checkGuess(){
		if((guess[1] == solution[1] && guess[2] == solution[2]) || (guess[1] != solution[1] && guess[2] != solution[2])){
			//print ("true");
			correct();
			return true;
		}
		//print("false");
		incorrect();
		return false;
	}

	private void rollBeans(){
		int leftOrRight = UnityEngine.Random.Range(1,3);
		int blackOrPinto = UnityEngine.Random.Range(1,3);

		solution [1] = leftOrRight;
		solution [2] = blackOrPinto;

		//print ("solution hand: " + leftOrRight);
		//print ("solution bean: " + blackOrPinto);

	}

	public float getMoney(){
		return money;
	}

	public void setMoney(float setMoney){
		money = setMoney;
	}

	public void addMoney(float addMoney){
		money += addMoney;
	}

	public void subtractMoney(float subMoney){
		money -= subMoney;
	}

	public int getStreak(){
		return currentStreak;
	}

	public int getLastStreak(){
		return lastStreak;
	}

	public void setLastStreak(int last){
		lastStreak = last;
	}

	public int getTopStreak(){
		return topStreak;
	}

	public void setTopStreak(int top){
		topStreak = top;
	}

	public float getOdds(){
		float odds;
		if (currentStreak <= 0) {
			odds = 0;//-(Mathf.Pow(0.5f,(float)currentStreak));
		} else {
			odds = (Mathf.Pow(0.5f,(float)currentStreak));
			//print (odds);
		}
		return odds;
	}
	/*
	public int getSPM(){
		
		float newScorePerMinute = 123;
		//print (moneyValue.getMoney());

		if (minimumUpdateTime) {
			updateTime = Time.time;
		}

		if (sixtySecondsCounter < updateTime) {
			sixtySecondsCounter += Time.time;
			//print ("counting up" + sixtySecondsCounter);
			//float testSPM = (moneyValue.getMoney() - )/();
			//print ();
		} else {
			currentTime = Time.time;
			//money = moneyValue.getMoney ();

			newScorePerMinute = (money) / (currentTime);  //  $/m
			newScorePerMinute = newScorePerMinute * 60;  //  scorePerminute = scorePerMinute * 60s

			//print (currentTime);
			//print (newScorePerMinute + " = new spm");
			//print (currentMoney - initialMoney + " = delta money");
			//print (currentTime - initialTime + " = delta time");

			initialTime = Time.time;
			//money = moneyValue.getMoney ();  //its getting this every .5 seconds, i need a better way to tell the difference...
		}
		/*
		if (newScorePerMinute > scorePerMinute) {
			scorePerMinute = newScorePerMinute;
		}

		if (counter < updateTime) {
			counter += Time.deltaTime;
		} else {

			initialMoney = moneyValue.getMoney ();
			initialTime = Time.deltaTime;
			counter = 0;
		}

		float newScorePerMinte = 123;
		currentTime = Time.time%60;
		newScorePerMinute = (money) / (currentTime);  //  $/m
		newScorePerMinute = newScorePerMinute * 60;  //  scorePerminute = scorePerMinute * 60s

		float newSpm = 0;
		float moneyChange = getMoney () - moneySnapshot;
		if(threeSecondCounter >= 3){
			newSpm = ((moneyChange) / (threeSecondCounter)) * 20;
			threeSecondCounter = 0;
			moneySnapshot = getMoney ();
		} else {
			threeSecondCounter += Time.deltaTime;
		}
		newSpm = getSPM ();

		return (int)newSpm;
	}
	*/

	public int getSPM(){
		return currentSPM;
	}
	
//	public void calculateSPM(){
//		float newSpm = 0;
//		float moneyChange;
//		if(tenSecondCounter >= 10){
//			moneyChange = Mathf.Ceil(getMoney () - moneySnapshot);
//			tenSecondCounter = Time.deltaTime;
//			moneySnapshot = getMoney ();
//		} else {
//			tenSecondCounter += Time.deltaTime;
//			//newSpm = getSPM ();
//			//moneyChange = Mathf.Ceil(getMoney () - moneySnapshot);
//		} 
//		newSpm = ((moneyChange)*6)/60;
//		currentSPM = (int)newSpm;
//	}

	public void setSPM(){

	}

	public void showBeans(){
		black.SetActive(true);
		pinto.SetActive(true);
	}

	public void hideBeans(){
		black.SetActive (false);
		pinto.SetActive (false);
	}

	private void correct(){
		if (currentStreak <= 0) {
			//negative streak stuff
			currentStreak = 1;
		} else {
			currentStreak++;
		}
		if (currentStreak >= topStreak) {
			topStreak = currentStreak;
		}
		//WIN TEXT
		string winnerText = randomWinText();
		WinLose.text = winnerText;				//set correct message
		//ADD MONEY
		int winProfit = (int)Mathf.Pow (2, currentStreak);  //eventually 2 is replaced by (leftBeanProfit + rightBeanProfit)
		addMoney (winProfit);
		updateMoneyText ();
	}

	private void incorrect(){
		lastStreak = currentStreak;
		if (currentStreak >= 0) {
			currentStreak--;
		} else if (currentStreak < 0) {

		}
		string loserText = randomLoseText();
		WinLose.text = loserText;
		currentStreak = 0;
		int lossProfit = (int)Mathf.Pow(2,0);  //eventually 2 is replaced by (leftBeanProfit + rightBeanProfit)
		//print(lossProfit);
		addMoney (lossProfit);
		updateMoneyText();
	}

	private void updateMoneyText(){
		moneyText.text = "$" + money;
	}

	private void updateTopStreak(){
		topStreakText.text = topStreak.ToString();
	}

	private void updateCurrentStreak(){
		currentStreakText.text = currentStreak.ToString();
	}

	private void updateLastStreak(){
		lastStreakText.text = lastStreak.ToString();

	}

	private void updateOdds(){
		float multipliedOdds = (float)(getOdds ()*100);
		oddsText.text = multipliedOdds.ToString() + "%";
	}

	private void updateSPM(){
		spmText.text = getSPM() + "\n$/min";
	}

	private void updateAllStats(){
		//calculateSPM ();
		updateSPM ();
		updateOdds ();
		updateTopStreak ();
		updateCurrentStreak ();
		updateLastStreak ();
		updateMoneyText ();
		//save money
	}

	private string randomWinText() {
		string[] winTexts = { "Nice Job!", "Correct!", "You're on a streak!" };
		return winTexts [UnityEngine.Random.Range (0, winTexts.Length)];
	}

	private string randomLoseText() {
		string[] loseTexts = { "Not Today!", "Incorrect", "C'mon try again!", "Next one's a winner!" };
		return loseTexts [UnityEngine.Random.Range (0, loseTexts.Length)];
	}

	public void Save(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/playertestsaveinfo.dat");
		PlayerData data = new PlayerData ();
		data.coins = getMoney ();
		data.spm = getSPM ();
		data.lastStreak = getLastStreak ();
		data.topStreak = getTopStreak ();
		//setMoney(data.coins);

		bf.Serialize (file, data);
		file.Close ();//Takes serizalable class "file" and writes to our class "data"
		print ("You Saved");
	}

	public void Load(){
		if (File.Exists (Application.persistentDataPath + "/playertestsaveinfo.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/playertestsaveinfo.dat", FileMode.Open);
			PlayerData data = (PlayerData)bf.Deserialize (file);
			file.Close ();

			setMoney(data.coins);
			setLastStreak (data.lastStreak);
			setTopStreak (data.topStreak);
			setSPM ();

			print ("You loaded!");
		}
	}

	//Delete file path
	private string SaveFilePatch {
		get { return Application.persistentDataPath + "/playertestsaveinfo.dat"; }
	}

	public void deleteSaveData() {
		try {
			File.Delete(SaveFilePatch);
		}
		catch(Exception ex) {
			Debug.LogException (ex);
		}
	}

	public void clearStats() {
		try {
			if (File.Exists (Application.persistentDataPath + "/playertestsaveinfo.dat")) {
				BinaryFormatter bf = new BinaryFormatter ();
				FileStream file = File.Open (Application.persistentDataPath + "/playertestsaveinfo.dat", FileMode.Open);
				PlayerData data = (PlayerData)bf.Deserialize (file);
				file.Close ();

				data.coins = 0;
				data.spm = 0;
			}
		}catch(Exception ex) {
			Debug.LogException (ex);
		}
	}
		

}

[Serializable]
class PlayerData {
	public float coins;
	public float spm;
	public int lastStreak;
	public int topStreak;
	//float/int game time
}