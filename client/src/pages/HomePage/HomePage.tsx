import "./HomePage.css";
import battshipPoster from "../../assets/battleshipsposter.jpg";
const HomePage = () => {
  //set height of both the aside and the home div to be the same of the window height
  // set width of the aside to be 50% of the window width and the home div to be 50% of the window width
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
  window.addEventListener("resize", setWidth);
  setWidth();
  window.addEventListener("resize", setHeight);
  setHeight();

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
            <button className="new-game-btn">Create a Game</button>
            <button className="join-game-btn">Join a Game</button>
          </div>
        </div>
      </div>
    </div>
  );
};
export default HomePage;
