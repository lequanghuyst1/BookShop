const Enum = {
    Gender: {
        Male: 0,
        Female: 1,
        Other: 2,
    },
    FormMode: {
        Add: 0,
        Edit: 1,
        Clone: 2,
    },
    FormAccount: {
        Login: 0,
        Register: 1,
    },
    ORDER_STATUS: {
        WAIT_FOR_CONFIRMATION: 0,
        CONFIRMED: 1,
        SHIPPING: 2,
        DELIVERYED: 3,
        CANCELLED: 4,
    }
};

export default Enum;