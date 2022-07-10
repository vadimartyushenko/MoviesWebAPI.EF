import React from "react";

const ThInput = ({changeFilter, parameter}) => {
    var text = "Filter By " + parameter;
    return (
        <th>
            <input className='form-control m-2'
                onChange={changeFilter}
                placeholder={text} />
            {parameter}
        </th>
    );
};

export default ThInput;