import React from "react";
import { NavLink, Link, useNavigate } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import { logout } from "../reducers/auth";
import { Nav, Navbar, NavDropdown } from "react-bootstrap";
import UserLogo from "../assets/person.svg";

const CustomNavbar = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();

  const { isLoggedIn, userData } = useSelector((state) => state.auth);

  const handleLogoutClick = async () => {
    await dispatch(logout());
    navigate("/");
  };

  return (
    <header>
      <Navbar expand="md" className="navbar-dark bg-dark">
        <Navbar.Brand href="/#" className="ps-2">
          Happy Paws
        </Navbar.Brand>
        <Navbar.Toggle
          className="navbar-toggler"
          type="button"
          data-toggle="collapse"
          data-target="#navbarCollapse"
        >
          <span className="navbar-toggler-icon"></span>
        </Navbar.Toggle>
        <Navbar.Collapse id="navbarCollapse">
          <Nav className="mr-auto flex-grow-1">
            <li className="nav-item ms-2">
              <NavLink to="/" className="nav-link">
                Home
              </NavLink>
            </li>
            <li className="nav-item ms-2">
              <NavLink to="/prices" className="nav-link">
                Prices
              </NavLink>
            </li>
            {isLoggedIn && (
              <>
                <li className="nav-item ms-2">
                  <NavLink to="/doctors" className="nav-link">
                    Doctors
                  </NavLink>
                </li>
                <li className="nav-item ms-2">
                  <NavLink to="/pets" className="nav-link">
                    Pets
                  </NavLink>
                </li>
                <li className="nav-item ms-2">
                  <NavLink to="/timeslots" className="nav-link">
                    Timeslots
                  </NavLink>
                </li>
              </>
            )}
          </Nav>
          {isLoggedIn && userData ? (
            <>
              <NavDropdown
                className="text-light ms-2 me-4"
                id="nav-dropdowne"
                title={
                  <>
                    <img src={UserLogo} /> {userData.name}
                  </>
                }
                menuVariant="dark"
              >
                <NavDropdown.Item>
                  <Link
                    to="/user/edit"
                    className="text-light text-decoration-none"
                  >
                    Edit Profile
                  </Link>
                </NavDropdown.Item>
                <NavDropdown.Divider />
                <NavDropdown.Item>
                  <button
                    type="button"
                    className="btn text-danger p-0"
                    onClick={handleLogoutClick}
                  >
                    Logout
                  </button>
                </NavDropdown.Item>
              </NavDropdown>
            </>
          ) : (
            <>
              <Link
                to="/signin"
                className="btn btn-sm btn-outline-secondary me-3"
              >
                Sign in
              </Link>
              <Link to="/register" className="btn btn-sm btn-secondary me-3">
                Register
              </Link>
            </>
          )}
        </Navbar.Collapse>
      </Navbar>
    </header>
  );
};

export default CustomNavbar;
