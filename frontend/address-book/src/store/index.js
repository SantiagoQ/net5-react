import { applyMiddleware, compose, createStore } from 'redux';
import createReducer from './reducers';
import createSagaMiddleware from 'redux-saga';
import rootSaga from './sagas';


const sagaMiddleware = createSagaMiddleware();
const enhancer = compose(
    applyMiddleware(sagaMiddleware),
    //TODO - logger is applied here in the future
);

const store = createStore(createReducer(), enhancer);
store.asyncReducers = {};
sagaMiddleware.run(rootSaga)

export default store;