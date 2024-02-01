import Board from "../../components/Board/Board";

const GamePage: React.FC = () => {
  return (
    <>
      <h1
        style={{
          textAlign: "center",
        }}
      >
        Battleship
      </h1>
      <Board />
    </>
  );
};

export default GamePage;
