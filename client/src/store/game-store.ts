import { createSlice, PayloadAction } from "@reduxjs/toolkit";
interface IGameState {
    gameId: string;
    players: IPlayer[];
}
interface IPlayer {
    playerName: string;
    fleetPlacements: number[][];
}

const initialState: IGameState = { gameId: "", players: [] };

const gameSlice = createSlice({
    name: "game",
    initialState,
    reducers: {
        setGame: (state, action: PayloadAction<any>) => {

            state.gameId = action.payload.gameId;
            state.players = action.payload.players;
        },
        
        addPlayer: (state, action: PayloadAction<IPlayer>) => {
            state.players.push(action.payload);
        },
    },
});

export const gameActions = gameSlice.actions;
export default gameSlice.reducer;