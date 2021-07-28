import { combineReducers } from 'redux';
import contacts from './contacts.reducer';

const createReducer = (asyncReducers) =>
    combineReducers({
        contacts,
        ...asyncReducers
    });

export default createReducer;