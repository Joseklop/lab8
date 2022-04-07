using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using lab8.Models;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using ReactiveUI;

namespace lab8.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {

        ObservableCollection<Note> planned;
        public ObservableCollection<Note> Planned
        {
            get => planned;
            set => this.RaiseAndSetIfChanged(ref planned, value);
        }

        ObservableCollection<Note> inWork;
        public ObservableCollection<Note> InWork
        {
            get => inWork;
            set => this.RaiseAndSetIfChanged(ref inWork, value);
        }

        ObservableCollection<Note> done;
        public ObservableCollection<Note> Done
        {
            get => done;
            set => this.RaiseAndSetIfChanged(ref done, value);
        }

        public MainWindowViewModel()
        {
            Planned = new ObservableCollection<Note>();
            InWork = new ObservableCollection<Note>();
            Done = new ObservableCollection<Note>();
        }

        public void Clear()
        {
            Planned.Clear();
            InWork.Clear();
            Done.Clear();
        }
        
        public void SaveFile(string path)
        {
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine(Planned.Count);
                foreach(var note in Planned)
                {
                    sw.WriteLine(note.Header);
                    var lines = 1;
                    for (int index = note.Task.IndexOf('\n'); index>=0; index = note.Task.IndexOf('\n', index+1), lines++);
                    sw.WriteLine(lines);
                    sw.WriteLine(note.Task);
                    sw.WriteLine(note.Path);
                }
                sw.WriteLine(InWork.Count);
                foreach (var note in InWork)
                {
                    sw.WriteLine(note.Header);
                    var lines = 1;
                    for (int index = note.Task.IndexOf('\n'); index >= 0; index = note.Task.IndexOf('\n', index + 1), lines++) ;
                    sw.WriteLine(lines);
                    sw.WriteLine(note.Task);
                    sw.WriteLine(note.Path);
                }
                sw.WriteLine(Done.Count);
                foreach (var note in Done)
                {
                    sw.WriteLine(note.Header);
                    var lines = 1;
                    for (int index = note.Task.IndexOf('\n'); index >= 0; index = note.Task.IndexOf('\n', index + 1), lines++) ;
                    sw.WriteLine(lines);
                    sw.WriteLine(note.Task);
                    sw.WriteLine(note.Path);
                }
            }
        }

        public void LoadFile(string path)
        {
            if (!File.Exists(path)) return;
            Clear();
            using(StreamReader sr = File.OpenText(path))
            {
                var planCount = int.Parse(sr.ReadLine());
                for(int i=0; i< planCount; ++i)
                {
                    var title = sr.ReadLine();
                    var taskLines = int.Parse(sr.ReadLine());
                    var task = "";
                    for(int j=0; j<taskLines; ++j)
                    {
                        task += sr.ReadLine()+"\n";
                    }
                    task = task.Substring(0, task.Length - 1);
                    var img = sr.ReadLine();
                    var item = new Note();
                    item.Header = title;
                    item.Task = task;
                    item.setImage(img);
                    Planned.Add(item);
                }
                var inworkCount = int.Parse(sr.ReadLine());
                for (int i = 0; i < inworkCount; ++i)
                {
                    var title = sr.ReadLine();
                    var taskLines = int.Parse(sr.ReadLine());
                    var task = "";
                    for (int j = 0; j < taskLines; ++j)
                    {
                        task += sr.ReadLine() + "\n";
                    }
                    task = task.Substring(0, task.Length - 1);
                    var img = sr.ReadLine();
                    var item = new Note();
                    item.Header = title;
                    item.Task = task;
                    item.setImage(img);
                    InWork.Add(item);
                }
                var doneCount = int.Parse(sr.ReadLine());
                for (int i = 0; i < doneCount; ++i)
                {
                    var title = sr.ReadLine();
                    var taskLines = int.Parse(sr.ReadLine());
                    var task = "";
                    for (int j = 0; j < taskLines; ++j)
                    {
                        task += sr.ReadLine() + "\n";
                    }
                    task = task.Substring(0, task.Length - 1);
                    var img = sr.ReadLine();
                    var item = new Note();
                    item.Header = title;
                    item.Task = task;
                    item.setImage(img);
                    Done.Add(item);
                }
            }
        }

        public void DeleteImage(Note note)
        {
            if(!Planned.Remove(note))
            {
                if(!InWork.Remove(note))
                {
                    Done.Remove(note);
                }
            }
        }

        public void AddNotePlanned()
        {
            Planned.Add(new Note());
        }
        public void AddNoteInWork()
        {
            InWork.Add(new Note());
        }
        public void AddNoteDone()
        {
            Done.Add(new Note());
        }
    }
}
