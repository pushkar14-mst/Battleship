import "./HomePage.css";
import battshipPoster from "../../assets/battleshipsposter.jpg";
import { useEffect, useState } from "react";
import useApi from "../../hooks/apiHook";
const HomePage = () => {
  const [toggle, setToggle] = useState<string>("");
  const [gameCode, setGameCode] = useState<string>("");
  const [playerName, setPlayerName] = useState<string>("");
  const [loading, setLoading] = useState<boolean>(false);

  const { createGame } = useApi();
  const createNewGame = async (gameCode: string, playerName: string) => {
    setLoading(true);
    await createGame(gameCode, playerName);
    setLoading(false);
  };
  useEffect(() => {
    const setHeight = () => {
      const windowHeight = window.innerHeight;
      const home = document.querySelector(".home") as HTMLElement;
      const aside = document.querySelector(".aside-poster") as HTMLElement;
      home.style.height = `${windowHeight}px`;
      aside.style.height = `${windowHeight}px`;
    };
    const setWidth = () => {
      const windowWidth = window.innerWidth;
      const home = document.querySelector(".home") as HTMLElement;
      const aside = document.querySelector(".aside-poster") as HTMLElement;
      home.style.width = `${windowWidth / 2}px`;
      aside.style.width = `${windowWidth / 2}px`;
    };
    setHeight();
    setWidth();
    window.addEventListener("resize", () => {
      setHeight();
      setWidth();
    });
  }, []);

  return (
    <div>
      {/* <h1
        style={{
          textAlign: "center",
        }}
      >
        Battleships
      </h1> */}
      <div className="home-container">
        <div className="aside-poster">
          <img src={battshipPoster} alt="battleship poster" />
        </div>
        <div className="home">
          <h1>Let's Play Battleships!!</h1>
          <div className="game-options">
            {
              {
                "new-game": (
                  <div className="game-options">
                    <input
                      type="text"
                      placeholder="Enter your name"
                      className="new-game-input"
                      onChange={(e) => {
                        setPlayerName(e.target.value);
                      }}
                    />
                    <input
                      type="text"
                      placeholder="Enter game code"
                      className="new-game-input"
                      onChange={(e) => {
                        setGameCode(e.target.value);
                      }}
                    />
                    <button
                      onClick={async () => {
                        await createNewGame(gameCode, playerName);
                      }}
                      className="create-game-btn"
                    >
                      {loading ? "Creating Game..." : "Create Game"}
                    </button>
                    <button
                      onClick={() => {
                        setToggle("");
                      }}
                      className="cancel-btn"
                    >
                      Cancel
                    </button>
                  </div>
                ),
                "join-game": (
                  <div className="game-options">
                    <input
                      type="text"
                      placeholder="Enter your name"
                      className="join-game-input"
                    />
                    <input
                      type="text"
                      placeholder="Enter game code"
                      className="join-game-input"
                    />
                    <button
                      onClick={() => {
                        console.log("Joining a game");
                      }}
                      className="join-game-submit-btn"
                    >
                      Join Game
                    </button>
                    <button
                      onClick={() => {
                        setToggle("");
                      }}
                      className="cancel-btn"
                    >
                      Cancel
                    </button>
                  </div>
                ),
              }[toggle]
            }
            {toggle === "" && (
              <div className="game-options">
                <button
                  className="new-game-btn"
                  onClick={() => {
                    setToggle("new-game");
                  }}
                >
                  Create a Game
                </button>
                <button
                  className="join-game-btn"
                  onClick={() => {
                    setToggle("join-game");
                  }}
                >
                  Join a Game
                </button>
              </div>
            )}
          </div>
        </div>
      </div>
    </div>
  );
};
export default HomePage;
