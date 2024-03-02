import { useLocation } from "react-router";
import Board from "../../components/Board/Board";
import useApi from "../../hooks/apiHook";
import { useSelector } from "react-redux";
import { useEffect, useState } from "react";

const GamePage: React.FC = () => {
  const [loading, setLoading] = useState<boolean>(true);
  // const [retrievedGameFromServer, setRetrievedGameFromServer] = useState<any>(
  //   {}
  // );
  const game = useSelector((state: any) => state.game);
  const location = useLocation();
  const gameCode = location.state.gameCode;
  const playerName = location.state.playerName;
  const { retrieveGame } = useApi();
  const onRetrieveGame = async () => {
    // setLoading(true);
    await retrieveGame(gameCode);
  };
  useEffect(() => {
    if (game.players.length > 0) {
      // setRetrievedGameFromServer(game);
      setLoading(false);
    }
  }, [game]);
  useEffect(() => {
    onRetrieveGame();
  }, [gameCode]);

  return (
    <>
      <h1
        style={{
          textAlign: "center",
        }}
      >
        Battleship
      </h1>
      {loading ? (
        <h2>Loading...</h2>
      ) : (
        <>
          <h2 style={{ textAlign: "center" }}>Game Code: {gameCode}</h2>
          <h3 style={{ textAlign: "center" }}>Player: {playerName}</h3>
          <Board gameId={gameCode} playerName={playerName} />
        </>
      )}
    </>
  );
};

export default GamePage;
