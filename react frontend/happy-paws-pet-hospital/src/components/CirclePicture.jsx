import React from "react";

const CirclePicture = ({ imageSource, title, phoneNumber, email }) => {
  return (
    <div className="d-flex flex-column col-12 col-md-4 my-3 align-items-center row-gap-2">
      <img
        className="rounded-circle image"
        src={imageSource}
        width="200"
        height="200"
      />
      <h2>{title}</h2>
      <p>Phone number: {phoneNumber}</p>
      <p>Email: {email}</p>
    </div>
  );
};

export default CirclePicture;
