import moment from "moment/moment";

export const dateTimeFormatter = (stringDate) => {
  const date = new Date(stringDate);
  const dateFormatter = moment(date).format("DD/MMM/yyyy");
  return dateFormatter;
};

export const getTimeOfDate = (stringDate) => {
  const date = new Date(stringDate);
  const timeFormatter = moment(date).format("LT");
  return timeFormatter;
}
