import { useEffect, useState } from "react";
import axios from "axios";
import * as signalR from "@microsoft/signalr";
import "./Board.css";
const Board: React.FC = () => {
  const [selectedCell, setSelectedCell] = useState<number[][]>([]);

  let connection = new signalR.HubConnectionBuilder()
    .configureLogging(signalR.LogLevel.Debug)
    .withUrl("https://localhost:7175/playersHub", {
      skipNegotiation: true,
      transport: signalR.HttpTransportType.WebSockets,
    })
    .build();
  console.log(connection);

  useEffect(() => {
    connection
      .start()
      .then(() => {
        console.log("Connection established");
        connection.invoke("SendFleetPlacements", {
          fleetPlacements: selectedCell,
        });
        connection.on("ReceiveFleetsFromServer", (message, data) => {
          console.log(message, data);
        });
      })
      .catch((error) => {
        console.error("Error establishing connection:", error);
      });
  }, []);

  console.log(selectedCell);
  const sendFleetPlacementsToServer = async () => {
    await axios.post("https://localhost:7175/Battleship/GetFleets", {
      fleetPlacements: selectedCell,
    });
  };

  useEffect(() => {
    sendFleetPlacementsToServer();
  }, [selectedCell]);

  return (
    <>
      <div className="board-container">
        <div className="board">
          {Array(10)
            .fill(0)
            .map((_, i) => (
              <div className="board-row">
                {Array(10)
                  .fill(0)
                  .map((_, j) => (
                    <div
                      className="board-cell"
                      style={{
                        backgroundColor: selectedCell.some(
                          (cell) => cell[0] === i && cell[1] === j
                        )
                          ? "#81cdc6"
                          : "#b3e0dc",
                      }}
                      onClick={() => {
                        setSelectedCell((prevState) => {
                          if (
                            prevState.some(
                              (cell) => cell[0] === i && cell[1] === j
                            )
                          ) {
                            return prevState.filter(
                              (cell) => cell[0] !== i || cell[1] !== j
                            );
                          }
                          return [...prevState, [i, j]];
                        });
                      }}
                    >
                      {selectedCell.some(
                        (cell) => cell[0] === i && cell[1] === j
                      ) ? (
                        <>
                          <img src="data:image/png;base64,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" />
                        </>
                      ) : (
                        ""
                      )}
                    </div>
                  ))}
              </div>
            ))}
        </div>
      </div>
    </>
  );
};
export default Board;
