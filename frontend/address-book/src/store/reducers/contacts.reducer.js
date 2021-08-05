

const initialState = {
    list: [],
    letters: ["A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T",
        "U", "V", "W", "X", "Y", "Z"],
};

const contactsReducer = function (state = initialState, action) {

    switch (action.type) {

        default: {
            return state;
        }
    }
}


export default contactsReducer;