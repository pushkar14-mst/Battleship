import axios from "axios";
import { useDispatch } from "react-redux";
import { useNavigate } from "react-router";
import { gameActions } from "../store/game-store";
const useApi = () => {
  const navigate = useNavigate();
  const dispatch = useDispatch();
  const createGame = async (gameCode: string, playerName: string) => {
    await axios
      .post("https://localhost:7175/Battleship/NewGame", {
        game: {
          gameId: gameCode,
          players: [{ playerName: playerName }],
        },
      })
      .then(() => {
        navigate("/game", {
          state: { gameCode: gameCode, playerName: playerName },
        });
      });
  };
  const retrieveGame = async (gameCode: string) => {
    await axios
      .get("https://localhost:7175/Battleship/RetrieveGame", {
        params: {
          gameCode,
        },
      })
      .then((response: any) => {
        let game: any = response.data.game;
        console.log(game);

        dispatch(gameActions.setGame(game));
      });
  };
  const joinGame = async (gameCode: string, playerName: string) => {
    console.log("in join game api function", gameCode, playerName);

    const response = await axios
      .post("https://localhost:7175/Battleship/AddPlayerToGame", {
        PlayerName: playerName,
        GameId: gameCode,
      })
      .then(async () => {
        await retrieveGame(gameCode);
        navigate("/game", {
          state: { gameCode: gameCode, playerName: playerName },
        });
      });
    console.log(response);
  };

  return {
    createGame,
    retrieveGame,
    joinGame,
  };
};

export default useApi;
