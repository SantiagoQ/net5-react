import { useDispatch } from 'react-redux';

export const GET_ALL_CONTACTS = '[CONTACTS] GET_ALL_CONTACTS';

export default function useContactsActions() {
    const dispatch = useDispatch();

    const getAllContacts = () => {
        dispatch({
            type: GET_ALL_CONTACTS,
        })
    }


    const api = {
        getAllContacts
    }
    return api;
}