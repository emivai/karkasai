import { useNavigate } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import { useEffect, useState } from "react";
import { logout } from "../reducers/auth";
import { deleteUser, editUser } from "../reducers/user";

const UserEdit = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();

  const { userData } = useSelector((state) => state.auth);

  const [editValue, setEditValue] = useState({
    name: "",
    surname: "",
    type: 1,
    photo: "",
    email: "",
    phoneNumber: "",
  });

  function handleEditChange(v) {
    const { id, value } = v.target;
    setEditValue({ ...editValue, [id]: value });
  }

  function handleNumericChange(v) {
    const { id, value } = v.target;
    setEditValue({ ...editValue, [id]: Number(value) });
  }

  const handleSaveClick = async () => {
    if (editValue.name !== "") {
      await dispatch(editUser({ id: userData.id, value: editValue }));
      navigate("/signin");
    }
  };

  const handleDeleteClick = async () => {
    if (editValue.name !== "") {
      await dispatch(deleteUser(userData.id));
      await dispatch(logout());
      navigate("/");
    }
  };

  useEffect(() => {
    if (userData) {
      setEditValue({
        name: userData.name,
        surname: userData.surname,
        type: userData.type,
        photo: userData.photo,
        email: userData.email,
        phoneNumber: userData.phoneNumber,
      });
    }
  }, [userData]);

  return (
    <div className="container d-flex align-items-center justify-content-center mt-5">
      <div className="form">
        <h1 className="h3 mb-3 font-weight-normal">Edit</h1>
        <label className="sr-only">Name</label>
        <input
          type="text"
          id="name"
          className="form-control"
          placeholder="Name"
          required
          value={editValue.name}
          onChange={(e) => handleEditChange(e)}
        />
        <label className="sr-only">Surname</label>
        <input
          type="text"
          id="surname"
          className="form-control"
          placeholder="Surname"
          required
          value={editValue.surname}
          onChange={(e) => handleEditChange(e)}
        />
        <label className="sr-only">Photo</label>
        <input
          type="text"
          id="photo"
          className="form-control"
          placeholder="Photo url"
          required
          value={editValue.photo}
          onChange={(e) => handleNumericChange(e)}
        />
        <label className="sr-only">Email address</label>
        <input
          type="email"
          id="email"
          className="form-control"
          placeholder="Email address"
          required
          value={editValue.email}
          onChange={(e) => handleEditChange(e)}
        />
        <label className="sr-only">Phone number</label>
        <input
          type="tel"
          id="phoneNumber"
          className="form-control"
          placeholder="+370"
          required
          value={editValue.phoneNumber}
          onChange={(e) => handleEditChange(e)}
        />
        <div className="d-flex gap-4">
          <button
            className="btn mt-4 btn-lg btn-outline-secondary btn-block flex-grow-1"
            type="button"
            onClick={handleSaveClick}
          >
            Save
          </button>
          <button
            className="btn mt-4 btn-lg btn-danger btn-block"
            type="button"
            onClick={handleDeleteClick}
          >
            Delete
          </button>
        </div>
      </div>
    </div>
  );
};

export default UserEdit;
