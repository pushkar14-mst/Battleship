import { useLocation } from "react-router";
import Board from "../../components/Board/Board";
import useApi from "../../hooks/apiHook";
import { useSelector } from "react-redux";
import { useEffect } from "react";

const GamePage: React.FC = () => {
  const location = useLocation();
  const gameCode = location.state.gameCode;
  const { retrieveGame } = useApi();
  useEffect(() => {
    retrieveGame(gameCode);
  }, [gameCode]);
  const retrievedGameFromServer = useSelector((state: any) => state.game);
  console.log(retrievedGameFromServer);

  return (
    <>
      <h1
        style={{
          textAlign: "center",
        }}
      >
        Battleship
      </h1>
      <h2 style={{ textAlign: "center" }}>Game Code: {gameCode}</h2>
      <h3 style={{ textAlign: "center" }}>
        Player 1: {retrievedGameFromServer.players[0].playerName}
      </h3>
      <Board
        gameId={gameCode}
        playerName={retrievedGameFromServer.players[0].playerName}
      />
    </>
  );
};

export default GamePage;
