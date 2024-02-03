import { configureStore } from "@reduxjs/toolkit";
import gameReducer from "./game-store";
type storeType=ReturnType<typeof configureStore>;

const store:storeType=configureStore({
    reducer:{
        game:gameReducer,
    }
})

export default store;