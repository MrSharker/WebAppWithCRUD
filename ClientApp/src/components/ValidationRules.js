export const validateEmail = (email) => {
    // Email validation regex pattern
    const emailPattern = /^\w+([.-]?\w+)*@\w+([.-]?\w+)*(\.\w{2,3})+$/;
    return emailPattern.test(email);
};

export const validateCellphone = (cellphone) => {
    // Phone number validation regex patterns
    const cellphonePattern1 = /^05\d{8}$/;       // 10-digit phone number starting with 05
    const cellphonePattern2 = /^\+972\d{9}$/;   // 12-digit phone number starting with +972

    return cellphonePattern1.test(cellphone) || cellphonePattern2.test(cellphone);
};
