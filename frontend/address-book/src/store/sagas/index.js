import { all } from 'redux-saga/effects';
import contacts from './contacts.saga';

export default function* rootSaga() {
    yield all([
        contacts()
    ])
}
